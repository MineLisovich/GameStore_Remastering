using GameStore.DAL.Entities.History;
using GameStore.DAL.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities.Games
{
    /// <summary>
    /// Таблица: Отзывы об игре
    /// </summary>
    public class GameReview
    {
        /// <summary>
        /// Id отзыва
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Id пользователя который написал отзыв
        /// </summary>
        [ForeignKey(nameof(User))]
        public required string UserId { get; set; }
        public AppUser? User { get; set; }

        /// <summary>
        /// Id игры к которой написан отзыв
        /// </summary>
        [ForeignKey(nameof(Game))]
        public long GameId { get; set; }
        public Game? Game { get; set; }

        /// <summary>
        /// Отзыв
        /// </summary>
        public required string Review { get; set; }

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
        public List<HistoryGameReviews> HistoryGameReviews { get; set; } = new();

    }
}
