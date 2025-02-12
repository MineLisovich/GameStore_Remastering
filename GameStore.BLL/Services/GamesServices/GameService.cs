using AutoMapper;
using GameStore.BLL.DTO.Dictionaries;
using GameStore.BLL.DTO.Games;
using GameStore.BLL.Infrastrcture;
using GameStore.BLL.Infrastrcture.Singletons;
using GameStore.BLL.Predefined;
using GameStore.DAL.Domain;
using GameStore.DAL.Entities.Dictionaries;
using GameStore.DAL.Entities.Games;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GameStore.BLL.Services.GamesServices
{
    public class GameService : IGameService
    {
        private readonly GsDbContext _dbContext;
        private readonly IMapper _mapper;

        public GameService(GsDbContext gsDbContext, IMapper mapper)
        {
            _dbContext = gsDbContext;
            _mapper = mapper;
        }

        public async Task<List<GameDTO>> GetGamesAsync(string nameGame)
        {
            IQueryable<Game> sqlQuery = _dbContext.Games.Include(gk => gk.GameKeys);
            List<Game> games = new();
            if (nameGame == "")
            {
                games = await sqlQuery.Where(x=>x.IsDeleted == false).ToListAsync();
            }
            else
            {
                games = await sqlQuery.Where(x => x.Name.ToUpper().Contains(RepName(nameGame)) && x.IsDeleted == false).ToListAsync();
            }
            return _mapper.Map<List<GameDTO>>(games);
        }



        public async Task<GameDTO> GetGameByIdAsync(long id)
        {
            Game game = await _dbContext.Games.Where(x => x.Id == id)
                                              .Include(gr => gr.GameGanres)
                                              .Include(d => d.Developer)
                                              .Include(l => l.GameLabels)
                                              .Include(s => s.Screenshots)
                                              .FirstOrDefaultAsync();
            return _mapper.Map<GameDTO>(game);
        }


        public async Task<ResultServiceModel> CreateGameAsync(GameDTO game)
        {
            ResultServiceModel result = new();

            //Проверка на дубликат игры
            bool isClone = await _dbContext.Games.AnyAsync(x => x.Name == game.Name);
            if (isClone is true) { result.IsSucceeded = false; result.ErrorMes = DefaultErrorMessages.clone; return result; }

            Game newGame = new() { Name = game.Name };
            newGame.Description = game.Description;
            newGame.GameGanres = await GetSelectedGanre(game.GameGanresIds);
            newGame.DeveloperId = game.DeveloperId;
            newGame.GameLabels = await GetSelectedLabels(game.GameLabelsIds);
            newGame.ReleaseDate = game.ReleaseDate;
            newGame.Price = game.Price;
            newGame.IsShare = false;
            newGame.SharePrice = null;
            newGame.YtLinkGameTrailer = null;
            newGame.Os = game.Os;
            newGame.Gpu = game.Gpu;
            newGame.Cpu = game.Cpu;
            newGame.Ram = game.Ram;
            newGame.Weight = game.Weight;
            newGame.IsDeleted = false;
            newGame.IsVisible = (game.IsConfirmActions is true) ? true : false;
            //Медиа файлы
            newGame.PosterName = (game.UploadPoster is not null) ? game.UploadPoster.FileName : null;
            newGame.Poster = (game.UploadPoster is not null) ? UploadPoster(game.UploadPoster) : null;
            newGame.Screenshots = (game.UploadScreenshots is not null) ? UploadGameScreenshot(game.UploadScreenshots) : null;
            newGame.GameKeys = (game.UploadGameKeys is not null) ? UploadGameKeys(game.UploadGameKeys) : null;
            newGame.isShowInSlider = game.isShowInSlider;
            newGame.SliderName = (game.UploadSliderImg is not null) ? game.UploadSliderImg.FileName : null;
            newGame.SliderImg = (game.UploadSliderImg is not null) ?  UploadPoster(game.UploadSliderImg) : null;

            try
            {
                _dbContext.Games.Update(newGame);
                await _dbContext.SaveChangesAsync();
            }
            catch { result.IsSucceeded = false; result.ErrorMes = DefaultErrorMessages.dontSave; return result; }

            result.IsSucceeded = true;
            return result;
        }

        public async Task<ResultServiceModel> UpdateGameAsync(GameDTO game)
        {
            ResultServiceModel result = new();

            Game currentGame = await _dbContext.Games.Where(x => x.Id == game.Id)
                                              .Include(gr => gr.GameGanres)
                                              .Include(d => d.Developer)
                                              .Include(l => l.GameLabels)
                                              .Include(s => s.Screenshots)
                                              .Include(k => k.GameKeys)
                                              .FirstOrDefaultAsync();

            if (currentGame is null) { result.IsSucceeded = false; result.ErrorMes = DefaultErrorMessages.recordNoExist; return result; }
            currentGame.Name = game.Name;
            currentGame.Description = game.Description;
            currentGame.GameGanres = await GetSelectedGanre(game.GameGanresIds);
            currentGame.DeveloperId = game.DeveloperId;
            currentGame.GameLabels = await GetSelectedLabels(game.GameLabelsIds);
            currentGame.ReleaseDate = game.ReleaseDate;
            currentGame.Price = game.Price;
            currentGame.YtLinkGameTrailer = null;
            currentGame.Os = game.Os;
            currentGame.Gpu = game.Gpu;
            currentGame.Cpu = game.Cpu;
            currentGame.Ram = game.Ram;
            currentGame.Weight = game.Weight;
            currentGame.isShowInSlider = game.isShowInSlider;

            //Медиа файлы
            if (game.UploadPoster is not null)
            {
                currentGame.PosterName = game.UploadPoster.FileName;
                currentGame.Poster = UploadPoster(game.UploadPoster);
            }
         
            if(game.UploadScreenshots is not null)
            {
                currentGame.Screenshots = UploadGameScreenshot(game.UploadScreenshots);
            }
        
            if (game.UploadGameKeys is not null)
            {
                List<GameKey> newKeys = UploadGameKeys(game.UploadGameKeys);
                currentGame.GameKeys.AddRange(newKeys);
            }

            if(game.UploadSliderImg is not null)
            {
             
                currentGame.SliderName = (game.UploadSliderImg is not null) ? game.UploadSliderImg.FileName : null;
                currentGame.SliderImg = (game.UploadSliderImg is not null) ? UploadPoster(game.UploadSliderImg) : null;
            }

            try
            {
                _dbContext.Games.Update(currentGame);
                await _dbContext.SaveChangesAsync();
            }
            catch { result.IsSucceeded = false; result.ErrorMes = DefaultErrorMessages.dontSave; return result; }

            result.IsSucceeded = true;
            return result;
        }

        public async Task<ResultServiceModel> FastUpdateGameAsync(GameDTO game)
        {
            ResultServiceModel result = new();

            Game currentGame = await _dbContext.Games.Where(x => x.Id == game.Id)
                                              .Include(k => k.GameKeys)
                                              .FirstOrDefaultAsync();

            if (currentGame is null) { result.IsSucceeded = false; result.ErrorMes = DefaultErrorMessages.recordNoExist; return result; }

            //Цена
            if(currentGame.Price != game.Price)
            {
                currentGame.Price = game.Price;
            }

            //Видимость на сайте
            if(currentGame.IsVisible != game.IsVisible)
            {
                currentGame.IsVisible = game.IsVisible;
            }

            //Акция 
            if(currentGame.IsShare != game.IsShare)
            {
                currentGame.IsShare = game.IsShare;
            }

            //Акция - цена
            if(currentGame.SharePrice != game.SharePrice)
            {
                currentGame.SharePrice = game.SharePrice;
            }

            //Ключи от игр
            if (game.UploadGameKeys is not null)
            {
                List<GameKey> newKeys = UploadGameKeys(game.UploadGameKeys);
                currentGame.GameKeys.AddRange(newKeys);
            }

            try
            {
                _dbContext.Games.Update(currentGame);
                await _dbContext.SaveChangesAsync();
            }
            catch { result.IsSucceeded = false; result.ErrorMes = DefaultErrorMessages.dontSave; return result; }

            result.IsSucceeded = true;
            return result;
        }

        public async Task<ResultServiceModel> DeleteGameAsync(long gameId, bool isSoft) 
        { 
            ResultServiceModel result = new();

            Game game = await _dbContext.Games.Where(x => x.Id == gameId).FirstOrDefaultAsync();
            if(game is null) { result.IsSucceeded = false; result.ErrorMes = DefaultErrorMessages.recordNoExist; return result; }

            if(isSoft is true)
            {
                game.IsDeleted = true;
                game.IsVisible = false;
            }
            else
            {
               _dbContext.Games.Remove(game);
            }

            try { await _dbContext.SaveChangesAsync(); }
            catch { result.IsSucceeded = false; result.ErrorMes = DefaultErrorMessages.dontSave; return result; }
           
            result.IsSucceeded = true;
            return result;
        }

        #region SELECT LIST
        public async Task<List<GameDeveloperDTO>> GetDevelopersForSelectList()
        {
            List<GameDeveloper> developers = await _dbContext.GameDevelopers.ToListAsync();
            return _mapper.Map<List<GameDeveloperDTO>>(developers);
        }
        public async Task<List<GenreDTO>> GetGenresForSelectList()
        {
            List<Genre> genres = await _dbContext.Genres.ToListAsync();
            return _mapper.Map<List<GenreDTO>>(genres);
        }
        public async Task<List<GamePlatformDTO>> GetPlatromsForSelectList()
        {
            List<GamePlatform> platforms = await _dbContext.GamePlatforms.ToListAsync();
            return _mapper.Map<List<GamePlatformDTO>>(platforms);
        }
        public async Task<List<GameLabelDTO>> GetLabelsForSelectList()
        {
            List<GameLabel> gameLabels = await _dbContext.GameLabels.ToListAsync();
            return _mapper.Map<List<GameLabelDTO>>(gameLabels);
        }
        #endregion

        #region PRIVATE METHODS
        private string RepName(string name)
        {
            return name.ToUpper().Trim();
        }
        private byte[] UploadPoster(IFormFile uploadPoster)
        {
            if (uploadPoster == null) return null;

            byte[] posterData = null;
            using (var stream = uploadPoster.OpenReadStream())
            using (var binaryReader = new BinaryReader(stream))
            {
                posterData = binaryReader.ReadBytes((int)uploadPoster.Length);
            }
            return posterData;

        }

        private List<GameScreenshot> UploadGameScreenshot(IFormFile[] uploadScreens)
        {
            if (uploadScreens == null) return new();

            List<GameScreenshot> screens = new();

            foreach (var screen in uploadScreens)
            {
                byte[] scData = null;
                using (var stream = screen.OpenReadStream())
                using (var binaryReader = new BinaryReader(stream))
                {
                    scData = binaryReader.ReadBytes((int)screen.Length);
                }

                GameScreenshot gameSc = new()
                {
                    Screenshot = scData,
                    ScreenshotName = screen.FileName,
                };
                screens.Add(gameSc);
            }

            return screens;
        }

        private List<GameKey> UploadGameKeys(IFormFile uploadKeys)
        {
            PredefinedManager pd = new();
            GameKeyCryptography gkc = GameKeyCryptography.GetIstance();
            List<GameKey> keys = new();
            if (uploadKeys != null && uploadKeys.Length > 0)
            {
                using (var reader = new StreamReader(uploadKeys.OpenReadStream()))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Разделяем строку по символу '/'
                        var parts = line.Split('/');
                        if (parts.Length == 2 && int.TryParse(parts[0], out int platformId))
                        {
                            string key = gkc.Encrypt(parts[1]);
                            keys.Add(new GameKey { PlatformId = platformId, Key = key, StatusId = pd.GameKeyStatuses.active.Id });
                        }
                    }
                }
            }
            return keys;
        }

        private async Task<List<Genre>> GetSelectedGanre(int[] ganreIds)
        {
            List<Genre> genres = new();

            if (ganreIds is not null)
            {
                foreach (int ganreId in ganreIds)
                {
                    Genre get = await _dbContext.Genres.Where(x => x.Id == ganreId).FirstOrDefaultAsync();
                    if (get != null)
                    {
                        genres.Add(get);
                    }
                }
            }
            return genres;
        }

        private async Task<List<GameLabel>> GetSelectedLabels(int[] gamesLabelsIds)
        {
            List<GameLabel> gameLabels = new();
            if (gamesLabelsIds is not null)
            {
                foreach (int gmLId in gamesLabelsIds)
                {
                    GameLabel get = await _dbContext.GameLabels.Where(x => x.Id == gmLId).FirstOrDefaultAsync();
                    if (get != null)
                    {
                        gameLabels.Add(get);
                    }
                }
            }
            return gameLabels;
        }
        #endregion
    }
}
