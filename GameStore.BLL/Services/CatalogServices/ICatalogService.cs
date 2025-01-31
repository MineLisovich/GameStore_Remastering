using GameStore.BLL.DTO.Dictionaries;
using GameStore.BLL.DTO.Games;
using GameStore.BLL.Infrastrcture.Models;

namespace GameStore.BLL.Services.CatalogServices
{
    public interface ICatalogService
    {
        Task<List<GameDTO>> GetGamesAsync(string nameGame);

        Task<List<GameDTO>> GetFromFilteredGamesAsync(FilterGamesRequest fgr);


        #region FOR SELECT LIST
        Task<List<GenreDTO>> GetGenresForSelectListAsync();
        Task<List<GameDeveloperDTO>> GetDevelopersForSelectListAsync();
        Task<List<GameLabelDTO>> GetLabelsForSelectListAsync();
        Task<List<GamePlatformDTO>> GetPlatformsForSelectListAsync();
        #endregion
    }
}
