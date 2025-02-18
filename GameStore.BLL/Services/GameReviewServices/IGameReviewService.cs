using GameStore.BLL.DTO.Games;
using GameStore.BLL.Infrastrcture;
using GameStore.BLL.Infrastrcture.Models;

namespace GameStore.BLL.Services.GameReviewServices
{
    public interface IGameReviewService
    {
        Task<ResultServiceModel> CreateReviewGameAsync(GameReviewModel model);
        Task<List<GameReviewDTO>> GetGameReviewsAsync(long gameId, int pageNumber, int pageSize);
        Task<ResultServiceModel> ElasticRemoveReviewAsync(long reviewId);
        Task<GameReviewDTO> GetGameReviewByIdAsync(long reviewId);
        Task<ResultServiceModel> UpdateGameReviewAsync(GameReviewDTO gameReview);
    }
}
