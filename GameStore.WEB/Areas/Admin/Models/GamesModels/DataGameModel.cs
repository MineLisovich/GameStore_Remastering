using GameStore.BLL.DTO.Games;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameStore.WEB.Areas.Admin.Models.GamesModels
{
    public class DataGameModel
    {
        public GameDTO Game { get; set; }

        public SelectList SelectListItemsGameGenres { get; set; }
        public SelectList SelectListItemsGameDevelopers { get; set; }
        public SelectList SelectListItemsGamePlatforms { get; set; }
        public SelectList SelectListItemsGameLabels { get; set; }
    }
}
