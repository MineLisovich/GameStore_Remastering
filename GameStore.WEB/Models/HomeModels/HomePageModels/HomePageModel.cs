using GameStore.BLL.DTO.Games;

namespace GameStore.WEB.Models.HomeModels.HomePageModels
{
    /// <summary>
    /// Модель главной страницы
    /// </summary>
    public class HomePageModel : HomePageBaseModel
    {
        /// <summary>
        /// Список: Новинки
        /// </summary>
        public List<GameDTO> NewGames { get; set; }

        /// <summary>
        /// Список: Лидеры продаж
        /// </summary>
        public List<GameDTO> SalesLeaders { get; set; }


        /// <summary>
        /// Список: Скоро выйдет
        /// </summary>
        public List<GameDTO> ComingSoon { get; set; }

    }
}
