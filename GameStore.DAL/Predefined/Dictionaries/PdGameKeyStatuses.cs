using GameStore.DAL.Entities.Dictionaries;

namespace GameStore.DAL.Predefined.Dictionaries
{
    public class PdGameKeyStatuses
    {
        /// <summary>
        /// Статус ключа: активный для покупки
        /// </summary>
        public GameKeyStatus active = new() { Id = 1, Name = "Активный" };

        /// <summary>
        /// Статус ключа: ключ забронирован
        /// </summary>
        public GameKeyStatus booked = new() { Id = 2, Name = "Забронирован" };

        /// <summary>
        /// Статус ключа: Неактивный
        /// </summary>
        public GameKeyStatus not_active = new () { Id = 3, Name = "Неактивный" };

        /// <summary>
        /// Список статусов
        /// </summary>
        public List<GameKeyStatus> ListGameKeyStatuses;

        public PdGameKeyStatuses()
        {
            ListGameKeyStatuses = new() {active, booked, not_active };
        }
    }
}
