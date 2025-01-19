using GameStore.BLL.DTO.Identity;
using GameStore.BLL.Infrastrcture;

namespace GameStore.BLL.Services.ShoppingCartServices
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartDTO> GetActiveShoppingCartAsync(string userEmail);
        Task<ResultServiceModel> AddToShoppingCartAsync(string userEmail, long gameId, int platformId);

        Task<ResultServiceModel> DeleteItemsInShoppingCartAsync(string userEmail, bool isDeleteAll, long gameKeyId = 0);

        Task<decimal> GetActualShoppingCartAsync(string userEmail);
    }
}
