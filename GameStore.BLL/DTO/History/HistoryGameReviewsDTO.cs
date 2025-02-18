using GameStore.BLL.DTO.Games;

/// <summary>
/// История изменения отзывов
/// </summary>
namespace GameStore.BLL.DTO.History
{
    public class HistoryGameReviewsDTO
    {
        public long Id { get; set; }

        /// <summary>
        /// Id отзыва
        /// </summary>
      
        public long ReviewId { get; set; }
        public GameReviewDTO GameReview { get; set; }

        /// <summary>
        /// Старое значения отзыва
        /// </summary>
        public string OldValue { get; set; } = null;

        /// <summary>
        /// Новое значение отзыва
        /// </summary>
        public string NewValue { get; set; } = null;

        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTime Date { get; set; }
    }
}
