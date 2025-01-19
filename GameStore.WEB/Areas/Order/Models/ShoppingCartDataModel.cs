using GameStore.BLL.DTO.Identity;
using GameStore.WEB.Infrastrcture;
using GameStore.WEB.Models;

namespace GameStore.WEB.Areas.Order.Models
{
    public class ShoppingCartDataModel
    {
        /// <summary>
        /// Корзина пользователя
        /// </summary>
        public ShoppingCartDTO ShoppingCart { get; set; }

        /// <summary>
        /// Результат действий пользователя
        /// </summary>
        public UserActionResult LastAction { get; set; } = new();

        /// <summary>
        /// Модель для вывода ошибок (пример: не удалось перейти на страницу игры)
        /// </summary>
        public ErrorViewModel ErrorVM { get; set; } = new();
    }
}
