using AutoMapper;
using GameStore.BLL.DTO.Identity;
using GameStore.BLL.Infrastrcture;
using GameStore.BLL.Predefined;
using GameStore.DAL.Domain;
using GameStore.DAL.Entities.Games;
using GameStore.DAL.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameStore.BLL.Services.ShoppingCartServices
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly GsDbContext _dbContext;
        private readonly IMapper _mapper;

        public ShoppingCartService (GsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ShoppingCartDTO> GetActiveShoppingCartAsync(string userEmail)
        {
           ShoppingCart shoppingCart = await GetActiveShCart(userEmail);
           return (shoppingCart is null) ? new() : _mapper.Map<ShoppingCartDTO>(shoppingCart);
        }


        public async Task<ResultServiceModel> AddToShoppingCartAsync(string userEmail, long gameId, int platformId)
        {
            ResultServiceModel result = new();
            PredefinedManager pd = new();

            //Шаг 1: Проверяем есть ли активная корзина
            ShoppingCart cart = await GetActiveShCart(userEmail);
            
            //Шаг 2: Если нет активной корзины то создатим её
            if(cart is null)
            {
                cart = await CreateShoppingCart(userEmail);
            }
            else
            {
                cart.UpdateDate = DateTime.UtcNow;
            }

            //Шаг 3: Добавляем игру в корзину
            GameKey gameKey = await _dbContext.GameKeys.Where(x=>x.GameId == gameId 
                                                           && x.PlatformId == platformId 
                                                           && x.StatusId == pd.GameKeyStatuses.active.Id)
                                                           .Include(x=>x.Game)
                                                           .FirstOrDefaultAsync();
            if(gameKey is null) { result.IsSucceeded = false; result.ErrorMes = "Для выбранной платформы отсутствуют ключи"; return result; }
           
            gameKey.StatusId = pd.GameKeyStatuses.booked.Id;
            cart.GamesKeys.Add(gameKey);
            cart.TotalPrice += (gameKey.Game.IsShare is true) ? gameKey.Game.SharePrice.Value : gameKey.Game.Price;

            try
            {
                _dbContext.ShoppingCarts.Update(cart);
                await _dbContext.SaveChangesAsync();
            }
            catch { result.IsSucceeded = false; result.ErrorMes = "Не удалось сохранить игру в корзину"; return result; }

            result.IsSucceeded = true;
            return result;
        }

        private async Task<ShoppingCart> GetActiveShCart(string userEmail)
        {
            AppUser user = await _dbContext.AppUsers.Where(x => x.Email == userEmail).FirstOrDefaultAsync();
            ShoppingCart shoppingCart = await _dbContext.ShoppingCarts.Where(x => x.UserId == user.Id && x.IsActive == true)
                                                                      .Include(x => x.GamesKeys).ThenInclude(x => x.Game)
                                                                      .FirstOrDefaultAsync();
            return shoppingCart;
        }

        private async Task<ShoppingCart> CreateShoppingCart(string userEmail)
        {
            AppUser user = await _dbContext.AppUsers.Where(x => x.Email == userEmail).FirstOrDefaultAsync();

            ShoppingCart cart = new() { UserId = user.Id };
            cart.CreateDate = DateTime.UtcNow;
            cart.IsActive = true;
            cart.TotalPrice = 0;
            cart.GamesKeys = null;

            await _dbContext.ShoppingCarts.AddAsync(cart);
            await _dbContext.SaveChangesAsync();    

            return cart;
        }
      
    }
}
