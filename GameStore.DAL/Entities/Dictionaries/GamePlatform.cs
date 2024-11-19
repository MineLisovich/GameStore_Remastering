using GameStore.DAL.Entities.Games;

namespace GameStore.DAL.Entities.Dictionaries
{
    /// <summary>
    /// Игровые платформы
    /// </summary>
    public class GamePlatform
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Наименование платформы
        /// </summary>
        public required string Name { get; set; }
    }
}
