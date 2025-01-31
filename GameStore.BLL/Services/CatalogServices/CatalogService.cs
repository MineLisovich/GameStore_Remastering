using AutoMapper;
using GameStore.BLL.DTO.Dictionaries;
using GameStore.BLL.DTO.Games;
using GameStore.BLL.Infrastrcture.Models;
using GameStore.DAL.Domain;
using GameStore.DAL.Entities.Dictionaries;
using GameStore.DAL.Entities.Games;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public async Task<List<GameDTO>> GetFromFilteredGamesAsync(FilterGamesRequest fgr)
        {
            IQueryable<Game> sqlQuery = _dbContext.Games
                                                  .Include(genre => genre.GameGanres)
                                                  .Include(dev => dev.Developer)
                                                  .Include(l => l.GameLabels)
                                                  .Include(k => k.GameKeys)
                                                  .Where(x => x.IsVisible && !x.IsDeleted);

            //ПО ЖАНРАМ
            if(fgr.Genres is not null && fgr.Genres.Count > 0)
            {
                sqlQuery = sqlQuery.Where(x => x.GameGanres.Any(g => fgr.Genres.Contains(g.Id)));
            }

            //ПО РАЗРАБАМ
            if(fgr.Develops is not null && fgr.Develops.Count > 0)
            {
                sqlQuery = sqlQuery.Where(x => fgr.Develops.Contains(x.DeveloperId));
            }

            //ПО ЦЕНЕ
            if(fgr.PriceFrom > 0 && fgr.PriceTo == 0)
            {
                sqlQuery = sqlQuery.Where(x=>x.Price >= fgr.PriceFrom);
            }
            else if (fgr.PriceTo > 0 && fgr.PriceFrom == 0)
            {
                sqlQuery = sqlQuery.Where(x => x.Price <= fgr.PriceTo);
            }
            else if (fgr.PriceFrom > 0 && fgr.PriceTo > 0)
            {
                sqlQuery = sqlQuery.Where(x => x.Price >= fgr.PriceFrom && x.Price <= fgr.PriceTo);
            }

            //ПО ОСОБЕННОСТЯМ
            if(fgr.Lables is not null && fgr.Lables.Count > 0)
            {
                sqlQuery = sqlQuery.Where(x => x.GameLabels.Any(g => fgr.Lables.Contains(g.Id)));
            }

            //ПО ПЛАТФОРМАМ
            if(fgr.Platforms is not null && fgr.Platforms.Count > 0)
            {
                sqlQuery = sqlQuery.Where(x => x.GameKeys.Any(g => fgr.Platforms.Contains(g.PlatformId)));
            }

            //СКИДОЧНАЯ ИГРА
            if(fgr.IsShare is true)
            {
                sqlQuery = sqlQuery.Where(x => x.IsShare == true);
            }

            // Получаем отфильтрованные игры
            List<Game> games = await sqlQuery.ToListAsync();

            return _mapper.Map<List<GameDTO>>(games);
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

        #region PRIVATE METHODS
        private string RepName(string name)
        {
            return name.ToLower().Trim();
        }
        #endregion
    }
}
