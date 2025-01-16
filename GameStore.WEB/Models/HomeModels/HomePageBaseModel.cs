using GameStore.BLL.DTO.Games;
using GameStore.WEB.Infrastrcture;

namespace GameStore.WEB.Models.HomeModels
{
    public class HomePageBaseModel
    {
        /// <summary>
        /// Список: Игры для слайдера : если IsSingleGamePage = false
        /// </summary>
        public List<GameDTO> SliderGames { get; set; } = new();

        /// <summary>
        /// Список скриншотов игры: если IsSingleGamePage = true
        /// </summary>
        public List<GameScreenshotDTO> Screenshots { get; set; } = new();

        /// <summary>
        /// Результат действий пользователя
        /// </summary>
        public UserActionResult LastAction { get; set; } = new();

        /// <summary>
        /// Мы находимся на странице игры?
        /// </summary>
        public bool IsSingleGamePage { get; set; } = false;

        /// <summary>
        /// Модель для вывода ошибок (пример: не удалось перейти на страницу игры)
        /// </summary>
        public ErrorViewModel ErrorVM { get; set; } = new();
    }
}
