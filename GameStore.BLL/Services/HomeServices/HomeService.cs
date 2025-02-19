﻿using AutoMapper;
using GameStore.BLL.DTO.Games;
using GameStore.BLL.Predefined;
using GameStore.DAL.Domain;
using GameStore.DAL.Entities.Games;
using GameStore.DAL.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameStore.BLL.Services.HomeServices
{
    public class HomeService : IHomeService
    {
        private readonly GsDbContext _dbContext;
        private readonly IMapper _mapper;

        public HomeService(GsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<GameDTO>> GetGamesWithSelectedTypeAsync(int selectedId)
        {
            PredefinedManager pd = new();
            List<Game> games = new();
            IQueryable<Game> sql = _dbContext.Games.Include(l => l.GameGanres).Include(k => k.GameKeys);

            if (selectedId == pd.SelectedGames.slider_games.OrderId)
            {
                games = await sql.Where(x => x.isShowInSlider == true && x.SliderImg.Length != 0 && x.IsDeleted == false && x.IsVisible == true).Take(5).ToListAsync();
            }
            else if (selectedId == pd.SelectedGames.new_games.OrderId)
            {
                games = await sql.OrderByDescending(x => x.DateAddedSite).Where(x => x.IsDeleted == false && x.IsVisible == true && x.GameKeys.Count() > 0).Take(5).ToListAsync();
            }
            else if (selectedId == pd.SelectedGames.sales_leaders.OrderId)
            {
                //Временно. Как появится статистика, изменить фильтрацию
                games = await sql.OrderBy(x => x.Price).Where(x => x.IsDeleted == false && x.IsVisible == true).Take(5).ToListAsync();
            }
            else
            {
                games = await sql.Where(x => DateTime.Today < x.ReleaseDate && x.IsDeleted == false && x.IsVisible == true).Take(5).ToListAsync();
            }

            return _mapper.Map<List<GameDTO>>(games);
        }

        public async Task<GameDTO> GetGameByIdAsync(long gameId)
        {
            Game game = await _dbContext.Games.Where(x => x.Id == gameId && x.IsDeleted == false && x.IsVisible == true)
                                              .Include(d => d.Developer)
                                              .FirstOrDefaultAsync();
            return _mapper.Map<GameDTO>(game);
        }

        public async Task<bool> IsCanAddToShoppingCart(long gameId)
        {
            PredefinedManager pd = new();
            return await _dbContext.GameKeys.AnyAsync(x => x.GameId == gameId && x.StatusId == pd.GameKeyStatuses.active.Id);
        }
        

        public async Task<GameDTO> GetGameByIdForPartial(long gameId)
        {
            Game game = await _dbContext.Games.Where(x => x.Id == gameId && x.IsDeleted == false && x.IsVisible == true)
                                             .Include(p => p.GameKeys).ThenInclude(x => x.Platform)
                                             .FirstOrDefaultAsync();
            return _mapper.Map<GameDTO>(game);
        }


        public async Task<string> GetUserInfoForHeader(string userEmail)
        {
            string userData = "";
            AppUser user = await _dbContext.AppUsers.Where(x => x.Email == userEmail).FirstOrDefaultAsync();
            if(user is not null) { userData = $"{user.CustomUserName}: {user.Balance} BYN"; }
            return userData;
        }

        public async Task<GameDTO> GetGamePartsAsync(long gameId, int partId)
        {
            PredefinedManager pd = new();
            IQueryable<Game> sql = _dbContext.Games.Where(x => x.Id == gameId && x.IsDeleted == false && x.IsVisible == true);
            if (partId == pd.GamePageParts.part_platforms.OrderId)
            {
                sql = sql.Include(p => p.GameKeys).ThenInclude(x => x.Platform);
            }
            else if (partId == pd.GamePageParts.part_genres.OrderId)
            {
                sql = sql.Include(g => g.GameGanres);
            }
            else if (partId == pd.GamePageParts.part_lables.OrderId)
            {
                sql = sql.Include(l => l.GameLabels);
            }
            else if (partId == pd.GamePageParts.part_screens.OrderId)
            {
                sql = sql.Include(s => s.Screenshots);
            }

            Game game = await sql.FirstOrDefaultAsync();

            return _mapper.Map<GameDTO>(game);
        }
    }
}
