using AutoMapper;
using GameStore.BLL.DTO.Dictionaries;
using GameStore.BLL.DTO.Games;
using GameStore.DAL.Domain;
using GameStore.DAL.Entities.Dictionaries;
using GameStore.DAL.Entities.Games;
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
                games = await sqlQuery.ToListAsync();
            }
            else
            {
                games = await sqlQuery.Where(x => x.Name.Contains(nameGame)).ToListAsync();
            }
            return _mapper.Map<List<GameDTO>>(games);
        }

        public async Task<GameDTO> GetGameByIdAsync(long id)
        {
            Game game = await _dbContext.Games.Where(x => x.Id == id)
                                              .Include(gr => gr.GameGanres)
                                              .Include(d => d.Developer)
                                              .Include(p => p.GamePlatforms)
                                              .Include(l => l.GameLabels)
                                              .Include(s => s.Screenshots)
                                              .FirstOrDefaultAsync();
            return _mapper.Map<GameDTO>(game);
        }




        //SELECT LIST
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
    }
}
