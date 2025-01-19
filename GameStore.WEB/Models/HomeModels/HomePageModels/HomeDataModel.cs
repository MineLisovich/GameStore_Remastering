using GameStore.BLL.DTO.Games;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GameStore.WEB.Models.HomeModels.HomePageModels
{
    public class HomeDataModel : HomePageBaseModel
    {
        public GameDTO Game { get; set; }

        [Required(ErrorMessage ="Это поле обязательно к заполнению!")]
        public int PlatformId { get; set; }
        public SelectList SelectItemsPlatforms { get; set; }
    }
}
