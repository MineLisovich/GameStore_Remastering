using GameStore.BLL.DTO.Dictionaries;

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
        /// Ключ активный? (то есть его ещё не купили)
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
