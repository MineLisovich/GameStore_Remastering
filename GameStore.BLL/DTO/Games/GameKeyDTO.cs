using GameStore.BLL.DTO.Dictionaries;
using GameStore.BLL.DTO.Identity;

namespace GameStore.BLL.DTO.Games
{
    /// <summary>
    /// Ключи от игр DTO
    /// </summary>
    public class GameKeyDTO
    {

        public long Id { get; set; }

        /// <summary>
        /// Ключ от игры
        /// </summary>
        public required string Key { get; set; }

        /// <summary>
        /// Id Игры
        /// </summary>
        public long GameId { get; set; }

        /// <summary>
        /// Игра
        /// </summary>
        public GameDTO Game { get; set; } = null;

        /// <summary>
        /// Id платформы
        /// </summary>
    
        public int PlatformId { get; set; }

        /// <summary>
        /// Игровая платформа
        /// </summary>
        public GamePlatformDTO Platform { get; set; } = null;


        /// <summary>
        /// Id стутуса ключа
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// Статус ключа
        /// </summary>
        public GameKeyStatusDTO GameKeyStatus { get; set; } = null;

        /// <summary>
        /// ID Корзины
        /// </summary>
        public long? ShoppingCartId { get; set; } = null;

        /// <summary>
        /// Корзина
        /// </summary>
        public ShoppingCartDTO ShoppingCart { get; set; } = null;
    }
}
