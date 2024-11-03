using GameStore.BLL.DTO.Dictionaries;
using GameStore.BLL.DTO.Games;
using GameStore.BLL.Infrastrcture;

namespace GameStore.BLL.Services.GamesServices
{
    public interface IGameService
    {
        Task<List<GameDTO>> GetGamesAsync(string nameGame);
        Task<GameDTO> GetGameByIdAsync(long id);
        Task<ResultServiceModel> CreateGameAsync(GameDTO game);
        Task<ResultServiceModel> UpdateGameAsync(GameDTO game);
        Task<ResultServiceModel> FastUpdateGameAsync(GameDTO game);
        Task<ResultServiceModel> DeleteGameAsync(long gameId, bool isSoft);
        
        
        //SELECT LIST
        Task<List<GameDeveloperDTO>> GetDevelopersForSelectList();
        Task<List<GenreDTO>> GetGenresForSelectList();
        Task<List<GamePlatformDTO>> GetPlatromsForSelectList();
        Task<List<GameLabelDTO>> GetLabelsForSelectList();

    }
}
