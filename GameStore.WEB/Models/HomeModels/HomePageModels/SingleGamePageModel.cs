using GameStore.BLL.DTO.Dictionaries;
using GameStore.BLL.DTO.Games;

namespace GameStore.WEB.Models.HomeModels.HomePageModels
{
    public class SingleGamePageModel : HomePageBaseModel
    {
        public GameDTO Game { get; set; }

        public List<GamePlatformDTO> Platforms { get; set; } = new();

        public List<GenreDTO> Genres { get; set; } = new();
        public List<GameLabelDTO> Labels { get; set; } = new();

        public bool IsCanAddToShoppCart { get; set; }
    }
}
