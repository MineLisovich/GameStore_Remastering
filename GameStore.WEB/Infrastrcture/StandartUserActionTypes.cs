namespace GameStore.WEB.Infrastrcture
{

    /// <summary>
    /// Стандартные действия пользователя в системе
    /// </summary>
    public class StandartUserActionTypes
    {
        /// <summary>
        /// Создание
        /// </summary>
        public readonly UserActionResult Create = new() { Id = 1, Name = "Создание" };

        /// <summary>
        /// Изменение
        /// </summary>
        public readonly UserActionResult Edit = new() { Id = 2, Name = "Изменение" };

        /// <summary>
        /// Удаление
        /// </summary>
        public readonly UserActionResult Delete = new() { Id = 3, Name = "Удаление" };

        /// <summary>
        /// Оповещение
        /// </summary>
        public readonly UserActionResult Notify = new() { Id = 4, Name = "Оповещение" };

        /// <summary>
        /// Смена пароля
        /// </summary>
        public readonly UserActionResult PasswordChange = new() { Id = 5, Name = "Смена пароля" };

        /// <summary>
        /// Подтверждение почты
        /// </summary>
        public readonly UserActionResult СonfirmEmail = new() { Id = 6, Name = "Подтверждение почты" };

        /// <summary>
        /// Отвязка почты
        /// </summary>
        public readonly UserActionResult UnlinkEmail = new() { Id = 7, Name = "Отвязка почты" };

        /// <summary>
        /// Включить 2fa
        /// </summary>
        public readonly UserActionResult Enable2FA = new() { Id = 8, Name = "Включить 2fa" };

        /// <summary>
        /// Выключить 2fa
        /// </summary>
        public readonly UserActionResult Disable2FA = new() { Id = 9, Name = "Выключить 2fa" };

        public readonly UserActionResult CleaerAllShoppingCart = new() {Id = 10, Name= "Полная очитска корзины" };

        public readonly UserActionResult AddGameToShoppingCart = new() { Id = 11, Name = "Добавление товара в корзину" };

        public readonly UserActionResult ReplenishBalance = new() { Id = 12, Name = "Пополнение счета" };
    }
}
