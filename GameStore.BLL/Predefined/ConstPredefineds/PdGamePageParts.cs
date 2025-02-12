namespace GameStore.BLL.Predefined.ConstPredefineds
{
    public class PdGamePageParts
    {
        public class GamePagePart
        {
            public int OrderId { get; set; }
            public string Name { get; set; }
        }

        public readonly GamePagePart part_platforms = new() {OrderId = 1, Name ="Платформы" };
        public readonly GamePagePart part_genres = new() { OrderId = 2, Name = "Жанры" };
        public readonly GamePagePart part_lables = new() { OrderId = 3, Name = "Особенности игры" };
        public readonly GamePagePart part_screens = new() { OrderId = 4, Name = "Скриншоты" };

        public readonly List<GamePagePart> GamePageParts;

        public PdGamePageParts()
        {
            GamePageParts = new() 
            {
               part_platforms,part_genres,part_lables,part_screens
            };
        }
    }
}
