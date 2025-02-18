namespace GameStore.BLL.Infrastrcture.Models
{
    public class GameReviewModel
    {
        public long GameId { get; set; }

        public string UserEmail { get; set; }

        public string Review { get; set; }
    }
}
