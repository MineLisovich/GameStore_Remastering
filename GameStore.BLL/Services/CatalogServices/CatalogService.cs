using AutoMapper;
using GameStore.BLL.DTO.Dictionaries;
using GameStore.BLL.DTO.Games;
using GameStore.DAL.Domain;
using GameStore.DAL.Entities.Dictionaries;
using GameStore.DAL.Entities.Games;
using Microsoft.EntityFrameworkCore;

namespace GameStore.BLL.Services.CatalogServices
{
    public class CatalogService : ICatalogService
    {
        private readonly GsDbContext _dbContext;
        private readonly IMapper _mapper;

        public CatalogService(GsDbContext gsDbContext, IMapper mapper)
        {
            _dbContext = gsDbContext;
            _mapper = mapper;
        }

        public async Task<List<GameDTO>> GetGamesAsync(string nameGame)
        {
            IQueryable<Game> sqlQuery = _dbContext.Games.Include(genre => genre.GameGanres)
                                                        .Include(dev => dev.Developer)
                                                        .Include(l => l.GameLabels)
                                                        .Include(k => k.GameKeys);
            List<Game> games = new();

            if (nameGame == "")
            {
                games = await sqlQuery.Where(x => x.IsVisible == true && x.IsDeleted == false).ToListAsync();
            }
            else
            {
                games = await sqlQuery.Where(x => x.Name.ToLower().Contains(RepName(nameGame)) && x.IsVisible == true && x.IsDeleted == false).ToListAsync();
            }


            return _mapper.Map<List<GameDTO>>(games);
        }

        private string RepName(string name)
        {
            return name.ToLower().Trim();
        }


        #region FOR SELECT LIST
        public async Task<List<GenreDTO>> GetGenresForSelectListAsync()
        {
            List<Genre> genres = await _dbContext.Genres.ToListAsync();
            return _mapper.Map<List<GenreDTO>>(genres);
        }
        public async Task<List<GameDeveloperDTO>> GetDevelopersForSelectListAsync()
        {
            List<GameDeveloper> devs = await _dbContext.GameDevelopers.ToListAsync();
            return _mapper.Map<List<GameDeveloperDTO>>(devs);
        }
        public async Task<List<GameLabelDTO>> GetLabelsForSelectListAsync()
        {
            List<GameLabel> lbls = await _dbContext.GameLabels.ToListAsync();
            return _mapper.Map<List<GameLabelDTO>>(lbls);
        }
        public async Task<List<GamePlatformDTO>> GetPlatformsForSelectListAsync()
        {
            List<GamePlatform> platforms = await _dbContext.GamePlatforms.ToListAsync();
            return _mapper.Map<List<GamePlatformDTO>>(platforms);
        }
        #endregion
    }
}
