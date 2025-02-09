using GameStore.BLL.DTO.Games;

namespace GameStore.BLL.Services.HomeServices
{
    public interface IHomeService
    {
        Task<List<GameDTO>> GetGamesWithSelectedTypeAsync(int selectedId);

        Task<GameDTO> GetGameByIdAsync(long gameId);

        Task<GameDTO> GetGameByIdForPartial(long gameId);

        Task<string> GetUserInfoForHeader(string userEmail);
    }
}
