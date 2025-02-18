using GameStore.DAL.Entities.Games;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities.History
{
    /// <summary>
    /// История изменения отзывов
    /// </summary>
    public class HistoryGameReviews
    {
        public long Id { get; set; }

        /// <summary>
        /// Id отзыва
        /// </summary>
        [ForeignKey(nameof(GameReview))]
        public long ReviewId { get; set; }
        public GameReview? GameReview { get; set; }

        /// <summary>
        /// Старое значения отзыва
        /// </summary>
        public string? OldValue { get; set; }

        /// <summary>
        /// Новое значение отзыва
        /// </summary>
        public string? NewValue { get; set; }

        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTime Date { get; set; }
    }
}
