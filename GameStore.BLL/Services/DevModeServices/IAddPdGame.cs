
using GameStore.BLL.Infrastrcture;

namespace GameStore.BLL.Services.DevModeServices
{
    public interface IAddPdGame
    {
        Task<ResultServiceModel> AddPdGames();
    }
}
