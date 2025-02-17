using GameStore.BLL.DTO.Identity;

namespace GameStore.BLL.DTO.Games
{
    /// <summary>
    /// Таблица: Отзывы об игре DTO
    /// </summary>
    public class GameReviewDTO
    {
        /// <summary>
        /// Id отзыва
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Id пользователя который написал отзыв
        /// </summary>
        public string UserId { get; set; }
        public AppUserDTO User { get; set; }

        /// <summary>
        /// Id игры к которой написан отзыв
        /// </summary>
        public long GameId { get; set; }
        public GameDTO Game { get; set; }

        /// <summary>
        /// Отзыв
        /// </summary>
        public string Review { get; set; }

        /// <summary>
        /// Дата создания отзыва
        /// </summary>
        public DateTime DateCreate { get; set; }

        /// <summary>
        /// Дата изменения отзыва
        /// </summary>
        public DateTime? DateModified { get; set; } = null;

        /// <summary>
        /// Отзыв положительный? (это поле будет заполняться автоматически)
        /// </summary>
        public bool isPositive { get; set; }

        /// <summary>
        /// Отзыв удалён (мягкое удаление)
        /// </summary>
        public bool isDelete { get; set; }

        /// <summary>
        /// История изменений отзыва
        /// </summary>
    }
}
