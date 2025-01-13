using GameStore.DAL.Entities.Dictionaries;
using GameStore.DAL.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities.Games
{
    /// <summary>
    /// Ключи от игр
    /// </summary>
    public class GameKey
    {
        public long Id { get; set; }
        
        /// <summary>
        /// Ключ от игры
        /// </summary>
        public required string Key { get; set; }
      
        /// <summary>
        /// Id Игры
        /// </summary>
        [ForeignKey(nameof(Game))]        
        public long GameId { get; set; }
        
        /// <summary>
        /// Игра
        /// </summary>
        public Game? Game { get; set; }

        /// <summary>
        /// Id платформы
        /// </summary>
        [ForeignKey(nameof(Platform))]
        public int PlatformId { get; set; }
        
        /// <summary>
        /// Игровая платформа
        /// </summary>
        public GamePlatform? Platform { get; set; }


        /// <summary>
        /// Id стутуса ключа
        /// </summary>
        [ForeignKey(nameof(GameKeyStatus))]
        public int StatusId { get; set; }
        
        /// <summary>
        /// Статус ключа
        /// </summary>
        public GameKeyStatus? GameKeyStatus { get; set; }

        /// <summary>
        /// ID Корзины
        /// </summary>
        [ForeignKey(nameof(ShoppingCart))]
        public long? ShoppingCartId { get; set; } = null;
        
        /// <summary>
        /// Корзина
        /// </summary>
        public ShoppingCart? ShoppingCart { get; set; }
    }
}
