using GameStore.BLL.DTO.Games;
using GameStore.WEB.Infrastrcture;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameStore.WEB.Models.HomeModels.CatalogModels
{
    public class CatalogModel
    {
        /// <summary>
        /// Список игр
        /// </summary>
        public List<GameDTO> Games { get; set; } = new();


        /// <summary>
        /// Поле для фильтров. Жанры игры
        /// </summary>
        public int[] GenreIds { get; set; }

        /// <summary>
        /// Выпадающий список. Жанры
        /// </summary>
        public SelectList SelectItemsGenres { get; set; } 
       
        /// <summary>
        /// Поле для фильтров. Разработчики игр
        /// </summary>
        public int[] DeveloperIds { get; set; }
        
        /// <summary>
        /// Выпадающий список. Разработчики
        /// </summary>
        public SelectList SelectItemsDevelopers { get; set; }

        /// <summary>
        /// Поле для фильтров. Цена от
        /// </summary>
        public decimal PriceFrom { get; set; }

        /// <summary>
        /// Поле для фильтров. Цена до
        /// </summary>
        public decimal PriceTo { get; set; }

        /// <summary>
        /// Поле для фильтров. Особенности игры
        /// </summary>
        public int[] GameLableIds { get; set; }

        /// <summary>
        /// Выпадающий список. Особенности игры
        /// </summary>
        public SelectList SelectItemsLables { get; set; }

        /// <summary>
        /// Поле для фильтров. Платформы
        /// </summary>
        public int[] PlatformIds { get; set; }

        /// <summary>
        /// Выпадающий список. Платформы
        /// </summary>
        public SelectList SelectItemsPlatforms { get; set; }

        /// <summary>
        /// Поле для фильтров. Продаётся по скидке
        /// </summary>
        public bool IsShare { get; set; }  
       
        /// <summary>
        /// Результат действий пользователя
        /// </summary>
        public UserActionResult LastAction { get; set; } = new();
    }
}
