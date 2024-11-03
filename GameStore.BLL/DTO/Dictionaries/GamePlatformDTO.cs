using GameStore.BLL.DTO.Games;
using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.DTO.Dictionaries
{
    /// <summary>
    /// Игровые платформы DTO
    /// </summary>
    public class GamePlatformDTO
    {

        public int Id { get; set; }

        /// <summary>
        /// Наименование платформы
        /// </summary>
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        public string Name { get; set; }
        public List<GameDTO> Games { get; set; } = new();
    }
}
