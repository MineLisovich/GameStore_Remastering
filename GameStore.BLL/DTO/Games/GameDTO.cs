using GameStore.BLL.DTO.Dictionaries;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.DTO.Games
{
    /// <summary>
    /// Игра DTO
    /// </summary>
    public class GameDTO
    {

        public long Id { get; set; }

        /// <summary>
        /// Наименование игры
        /// </summary>
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        public required string Name { get; set; }

        /// <summary>
        /// Описание игры
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// Техническое поле. Временно хранит выбранные Id жанров из выподающего списка
        /// </summary>
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        public int[] GameGanresIds { get; set; }

        /// <summary>
        /// Список жанров
        /// </summary>
        public List<GenreDTO> GameGanres { get; set; } = new();


        /// <summary>
        /// Id Разработчика игры
        /// </summary>
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        public int DeveloperId { get; set; }

        /// <summary>
        /// Разработчик игры
        /// </summary>
        public GameDeveloperDTO Developer { get; set; } = null;


        /// <summary>
        /// Техническое поле. Временно хранит выбранные Id игровых платформ из выподающего списка
        /// </summary>
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        public int[] GamePlatformsIds { get; set; }

        /// <summary>
        /// Список игровых платформ
        /// </summary>
        public List<GamePlatformDTO> GamePlatforms { get; set; } = new();

        /// <summary>
        /// Дата выхода игры
        /// </summary>

        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        public decimal Price { get; set; }

        /// <summary>
        /// Скидочная игра
        /// </summary>
        public bool IsShare { get; set; } = false;

        /// <summary>
        /// Скидочная цена
        /// </summary>
        public decimal? SharePrice { get; set; }

        /// <summary>
        /// Постер (картинка)
        /// </summary>
        public byte[] Poster { get; set; } = null;

        /// <summary>
        /// Наименование постера
        /// </summary>
        public string PosterName { get; set; } = null;

        /// <summary>
        /// Техническое поле. Временно хранит выбранную картинку постера
        /// </summary>
        public IFormFile UploadPoster { get; set; }

        /// <summary>
        /// Ссылка на ютуб трейлер игры
        /// </summary>
        public string YtLinkGameTrailer { get; set; } = null;

        /// <summary>
        /// Скриншоты игры
        /// </summary>
        public List<GameScreenshotDTO> Screenshots { get; set; } = new();

        /// <summary>
        /// Техническое поле. Временно хранит выбранные картинки скриншотов игры
        /// </summary>
        public IFormFile[] UploadScreenshots { get; set; }

        /// <summary>
        /// Ключи от игры
        /// </summary>
        public List<GameKeyDTO> GameKeys { get; set; } = new();


        /// <summary>
        /// Техническое поле. Временно хранит выбранный файл с ключаси от игры
        /// </summary>
        public IFormFile UploadGameKeys { get; set; }

        /// <summary>
        /// Ярлыки игры
        /// </summary>
        public List<GameLabelDTO> GameLabels { get; set; } = new();


        /// <summary>
        /// Техническое поле. Временно хранит выбранные Id ярлыков игры из выподающего списка
        /// </summary>
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        public int[] GameLabelsIds { get; set; }

        //Системные требование игры
        /// <summary>
        /// Форма записи: Рекомендуемые / Минимальные. Операционная система
        /// </summary>
        public string Os { get; set; } = null;

        /// <summary>
        /// Форма записи: Рекомендуемые / Минимальные. Процессор
        /// </summary>
        public string Cpu { get; set; } = null;

        /// <summary>
        /// Форма записи: Рекомендуемые / Минимальные. Оперативная память
        /// </summary>
        public string Ram { get; set; } = null;

        /// <summary>
        /// Занимаймое место на диске
        /// </summary>
        public int? Weight { get; set; }

        //Технические поля
        /// <summary>
        /// Дата добавления
        /// </summary>
        public DateTime DateAddedSite { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Видимость на сайте
        /// </summary>
        public bool IsVisible { get; set; } = false;

        /// <summary>
        /// Игра удалена (фэйковое удаление игры)
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Техническое поле. Подтверждение что данные все данные введены 
        /// </summary>
        public bool IsConfirmActions { get; set; } = false;
    }
}
