namespace GameStore.DAL.Entities.Dictionaries
{
    /// <summary>
    /// Статус ключа от игры
    /// </summary>
    public class GameKeyStatus
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Наименование статуса
        /// </summary>
        public required string Name { get; set; }
    }
}
