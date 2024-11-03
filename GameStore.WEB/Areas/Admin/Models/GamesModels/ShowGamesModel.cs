using GameStore.BLL.DTO.Games;
using GameStore.WEB.Infrastrcture;

namespace GameStore.WEB.Areas.Admin.Models.GamesModels
{
    public class ShowGamesModel
    {
        public List<GameDTO> Games { get; set; }    
        public UserActionResult LastAction { get; set; }
    }
}
