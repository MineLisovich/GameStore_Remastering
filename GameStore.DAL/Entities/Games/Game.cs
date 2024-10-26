
using GameStore.DAL.Entities.Dictionaries;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities.Games
{
    /// <summary>
    /// Игра
    /// </summary>
    public class Game
    {
        public long Id { get; set; }

        /// <summary>
        /// Наименование игры
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Описание игры
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Список жанров
        /// </summary>
        public List<Genre> GameGanres { get; set; } = new();

        /// <summary>
        /// Id Разработчика игры
        /// </summary>
        [ForeignKey(nameof(Developer))]
        public int DeveloperId { get; set; }

        /// <summary>
        /// Разработчик игры
        /// </summary>
        public GameDeveloper? Developer { get; set; }

        /// <summary>
        /// Список игровых платформ
        /// </summary>
        public List<GamePlatform> GamePlatforms { get; set; } = new();

        /// <summary>
        /// Дата выхода игры
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
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
        public byte[]? Poster { get; set; }

        /// <summary>
        /// Наименование постера
        /// </summary>
        public string? PosterName { get; set; }

        /// <summary>
        /// Ссалка на ютуб трейлер игры
        /// </summary>
        public string? YtLinkGameTrailer { get; set; }

        /// <summary>
        /// Скриншоты игры
        /// </summary>
        public List<GameScreenshot> Screenshots { get; set; } = new();

        /// <summary>
        /// Ключи от игры
        /// </summary>
        public List<GameKey> GameKeys { get; set; } = new();

        /// <summary>
        /// Ярлыки игры
        /// </summary>
        public List<GameLabel> GameLabels { get; set; } = new();

        //Системные требование игры
        /// <summary>
        /// Форма записи: Рекомендуемые / Минимальные. Операционная система
        /// </summary>
        public string? Os { get; set; }

        /// <summary>
        /// Форма записи: Рекомендуемые / Минимальные. Процессор
        /// </summary>
        public string? Cpu { get; set; }


        /// <summary>
        /// Форма записи: Рекомендуемые / Минимальные. Видиокарта
        /// </summary>
        public string? Gpu { get; set; }

        /// <summary>
        /// Форма записи: Рекомендуемые / Минимальные. Оперативная память
        /// </summary>
        public string? Ram { get; set; }

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

    }
}
