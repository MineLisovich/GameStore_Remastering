using GameStore.BLL.DTO.Dictionaries;
using GameStore.BLL.DTO.Games;

namespace GameStore.BLL.Services.CatalogServices
{
    public interface ICatalogService
    {
        Task<List<GameDTO>> GetGamesAsync(string nameGame);


        #region FOR SELECT LIST
        Task<List<GenreDTO>> GetGenresForSelectListAsync();
        Task<List<GameDeveloperDTO>> GetDevelopersForSelectListAsync();
        Task<List<GameLabelDTO>> GetLabelsForSelectListAsync();
        Task<List<GamePlatformDTO>> GetPlatformsForSelectListAsync();
        #endregion
    }
}
