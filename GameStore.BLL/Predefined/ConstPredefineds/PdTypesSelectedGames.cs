namespace GameStore.BLL.Predefined.ConstPredefineds
{
    public class PdTypesSelectedGames
    {
        public class TypeSelectGame
        {
            public int OrderId { get; set; }
            public string Name { get; set; }
        }

        public readonly TypeSelectGame slider_games = new() { OrderId = 1, Name = "Слайдер" };
        public readonly TypeSelectGame new_games = new() { OrderId = 2, Name = "Новинки" };
        public readonly TypeSelectGame sales_leaders = new() { OrderId = 3, Name = "Лидеры продаж" };
        public readonly TypeSelectGame coming_soon = new() { OrderId = 4, Name = "Скоро выйдет" };

        public List<TypeSelectGame> TypeSelectGames;

        public PdTypesSelectedGames ()
        {
            TypeSelectGames = new()
            {
               slider_games, new_games, sales_leaders, coming_soon
            };
        }
    }
}
