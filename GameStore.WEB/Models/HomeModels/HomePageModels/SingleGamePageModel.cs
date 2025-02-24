using GameStore.BLL.DTO.Dictionaries;
using GameStore.BLL.DTO.Games;
using GameStore.BLL.Infrastrcture.Models;

namespace GameStore.WEB.Models.HomeModels.HomePageModels
{
    public class SingleGamePageModel : HomePageBaseModel
    {
        public GameDTO Game { get; set; }

        public List<GamePlatformDTO> Platforms { get; set; } = new();

        public List<GenreDTO> Genres { get; set; } = new();
        public List<GameLabelDTO> Labels { get; set; } = new();

        public bool IsCanAddToShoppCart { get; set; }

        public List<GameReviewDTO> GameReviews { get; set; } = new();

        public bool HasMoreReview { get; set; }

        public GameReviewDTO GameReview { get; set; }

        public GameReviewStatsModel  GameReviewStats { get; set; } = new();
    }
}
