using GameStore.DAL.Entities.Games;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities.Identity
{
    /// <summary>
    /// Корзина пользователя
    /// </summary>
    public class ShoppingCart
    {
        public long Id { get; set; }

        /// <summary>
        /// Состояние корзины (true - активная, false - не активная)
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Id пользователя
        /// </summary>
        [ForeignKey(nameof(User))]
        public required string UserId { get; set; }
       
        /// <summary>
        /// Пользователь
        /// </summary>
        public AppUser? User { get; set; }

        /// <summary>
        /// Игры
        /// </summary>
        public List<GameKey> GamesKeys { get; set; } = new();

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

        /// <summary>
        /// Дата оплаты корзины
        /// </summary>
        public DateTime? PaymentDate { get; set; } = null;
    }
}
