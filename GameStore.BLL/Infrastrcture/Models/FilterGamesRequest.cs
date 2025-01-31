
namespace GameStore.BLL.Infrastrcture.Models
{
    public class FilterGamesRequest
    {
        public List<int> Genres { get; set; }
        public List<int> Develops { get; set; }
        public decimal PriceFrom { get; set; } = 0;
        public decimal PriceTo { get; set; } = 0;
        public List<int> Lables { get; set; }
        public List<int> Platforms { get; set; }
        public bool IsShare { get; set; }
    }
}
