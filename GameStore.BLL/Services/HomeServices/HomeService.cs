using AutoMapper;
using GameStore.BLL.DTO.Games;
using GameStore.BLL.Predefined;
using GameStore.DAL.Domain;
using GameStore.DAL.Entities.Games;
using Microsoft.EntityFrameworkCore;

namespace GameStore.BLL.Services.HomeServices
{
    public class HomeService : IHomeService
    {
        private GsDbContext _dbContext;
        private IMapper _mapper;

        public HomeService(GsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<GameDTO>> GetGamesWithSelectedTypeAsync(int selectedId)
        {
            PredefinedManager pd = new();
            List<Game> games = new();
            IQueryable<Game> sql = _dbContext.Games.Include(l=>l.GameGanres).Include(k=>k.GameKeys);

            if(selectedId == pd.SelectedGames.slider_games.OrderId)
            {
                games = await sql.Where(x=>x.isShowInSlider == true && x.SliderImg.Length != 0 && x.IsDeleted == false && x.IsVisible == true).Take(5).ToListAsync();
            }
            else if(selectedId == pd.SelectedGames.new_games.OrderId)
            {
                 games = await sql.OrderByDescending(x=>x.DateAddedSite).Where(x=>x.IsDeleted == false && x.IsVisible == true && x.GameKeys.Count() > 0).Take(5).ToListAsync();
            }
            else if(selectedId == pd.SelectedGames.sales_leaders.OrderId)
            {
               //Временно. Как появится статистика, изменить фильтрацию
               games = await sql.OrderBy(x=>x.Price).Where(x=>x.IsDeleted == false && x.IsVisible == true).Take(5).ToListAsync();
            }
            else
            {
                games = await sql.Where(x=> DateTime.Today < x.ReleaseDate && x.IsDeleted == false && x.IsVisible == true).Take(5).ToListAsync();
            }

            return _mapper.Map<List<GameDTO>>(games);
        }

        public async Task<GameDTO> GetGameByIdAsync(long gameId)
        {
            Game game = await _dbContext.Games.Where(x=>x.Id == gameId && x.IsDeleted == false && x.IsVisible == true)
                                              .Include(d=>d.Developer)
                                              .Include(p=>p.GameKeys).ThenInclude(x=>x.Platform)
                                              .Include(g=>g.GameGanres)
                                              .Include(s=>s.Screenshots)
                                              .FirstOrDefaultAsync();
            return _mapper.Map<GameDTO>(game);
        }

       public async Task<GameDTO> GetGameByIdForPartial(long gameId)
       {
            Game game = await _dbContext.Games.Where(x => x.Id == gameId && x.IsDeleted == false && x.IsVisible == true)
                                             .Include(p => p.GameKeys).ThenInclude(x => x.Platform)
                                             .FirstOrDefaultAsync();
            return _mapper.Map<GameDTO>(game);
       }
    }
}
