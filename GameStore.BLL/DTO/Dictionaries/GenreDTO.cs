using GameStore.BLL.DTO.Games;
using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.DTO.Dictionaries
{
    /// <summary>
    /// Жанры игр DTO
    /// </summary>
    public class GenreDTO
    {
        public int Id { get; set; }

        /// <summary>
        /// Наименование жанра
        /// </summary>
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        public string Name { get; set; }

        public List<GameDTO> Games { get; set; } = new();
    }
}
