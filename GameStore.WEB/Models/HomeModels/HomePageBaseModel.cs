using GameStore.WEB.Infrastrcture;

namespace GameStore.WEB.Models.HomeModels
{
    public class HomePageBaseModel
    {
        /// <summary>
        /// Результат действий пользователя
        /// </summary>
        public UserActionResult LastAction { get; set; } = new();

        /// <summary>
        /// Мы находимся на странице игры?
        /// </summary>
        public bool IsSingleGamePage { get; set; } = false;
    }
}
