using GameStore.BLL.DTO.Games;
using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.DTO.Dictionaries
{

    /// <summary>
    /// Ярлыки игры (типо PVE PVP Coop и пр.) DTO
    /// </summary>
    public class GameLabelDTO
    {
        public int Id { get; set; }

        /// <summary>
        /// Наименование ярлыка
        /// </summary>
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        public string Name { get; set; }

        /// <summary>
        /// Игры
        /// </summary>
        public List<GameDTO> Games { get; set; } = new();
    }
}
