using GameStore.BLL.DTO.Games;

namespace GameStore.BLL.Services.CatalogServices
{
    public interface ICatalogService
    {
        Task<List<GameDTO>> GetGamesAsync(string nameGame);
    }
}
