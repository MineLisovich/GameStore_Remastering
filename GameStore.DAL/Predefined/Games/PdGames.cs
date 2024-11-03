using GameStore.DAL.Entities.Games;

namespace GameStore.DAL.Predefined.Games
{
    public class PdGames
    {
        public Game testGame_1 = new()
        {
            Id = 1,
            Name = "Grand The Auto 5",
            DeveloperId = 1,
            ReleaseDate = DateTime.UtcNow,
            Price = 34.33m,
        };

        public Game testGame_2 = new()
        {
            Id = 2,
            Name = "The First Player",
            DeveloperId = 9,
            ReleaseDate = DateTime.UtcNow,
            Price = 120.50m,
        };

        public Game testGame_3 = new()
        {
            Id = 3,
            Name = "The Wither 3",
            DeveloperId = 3,
            ReleaseDate = DateTime.UtcNow,
            Price = 79.00m,
        };

        public Game testGame_4 = new()
        {
            Id = 4,
            Name = "Just Dance",
            DeveloperId = 5,
            ReleaseDate = DateTime.UtcNow,
            Price = 66.33m,
        };

        public Game testGame_5 = new()
        {
            Id = 5,
            Name = "Far cry 5",
            DeveloperId = 2,
            ReleaseDate = DateTime.UtcNow,
            Price = 67.88m,
        };

        public List<Game> testGames;

        public PdGames()
        {
            testGames = new()
            {
                testGame_1, testGame_2, testGame_3, testGame_4, testGame_5
            };
        }

    }
}
