using GameStore.BLL.DTO.Games;

namespace GameStore.BLL.DTO.Identity
{
    /// <summary>
    /// Корзина пользователя DTO
    /// </summary>
    public class ShoppingCartDTO
    {
        public long Id { get; set; }

        /// <summary>
        /// Состояние корзины (true - активная, false - не активная)
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Id пользователя
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public AppUserDTO User { get; set; } = null;

        /// <summary>
        /// Игры
        /// </summary>
        List<GameKeyDTO> GamesKeys { get; set; } = new();

        /// <summary>
        /// Финальная стоимость корзины
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Дата создания корзины
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Дата последнего изменения корзины
        /// </summary>
        public DateTime? UpdateDate { get; set; } = null;
    }
}
