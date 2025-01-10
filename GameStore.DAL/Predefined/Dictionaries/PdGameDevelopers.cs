using GameStore.DAL.Entities.Dictionaries;

namespace GameStore.DAL.Predefined.Dictionaries
{
    /// <summary>
    /// Разработчики игр. Предзаданные значения
    /// </summary>
    public class PdGameDevelopers
    {
        public GameDeveloper d1 = new() { Id = 1, Name = "Epic Games" };
        public GameDeveloper d2 = new() { Id = 2, Name = "Electronic Arts" };
        public GameDeveloper d3 = new() { Id = 3, Name = "Rockstar Games" };
        public GameDeveloper d4 = new() { Id = 4, Name = "Ubisoft" };
        public GameDeveloper d5 = new() { Id = 5, Name = "Activision" };
        public GameDeveloper d6 = new() { Id = 6, Name = "Nintendo" };
        public GameDeveloper d7 = new() { Id = 7, Name = "Microsoft Studios" };
        public GameDeveloper d8 = new() { Id = 8, Name = "Sony Interactive Entertainment" };
        public GameDeveloper d9 = new() { Id = 9, Name = "Capcom" };
        public GameDeveloper d10 = new() { Id = 10, Name = "Square Enix" };
        public GameDeveloper d11 = new() { Id = 11, Name = "Sega" };
        public GameDeveloper d12 = new() { Id = 12, Name = "Bethesda Softworks" };
        public GameDeveloper d13 = new() { Id = 13, Name = "Warner Bros. Interactive Entertainment" };
        public GameDeveloper d14 = new() { Id = 14, Name = "Take-Two Interactive" };
        public GameDeveloper d15 = new() { Id = 15, Name = "Konami" };
        public GameDeveloper d16 = new() { Id = 16, Name = "11 Bit Studios" };
        public GameDeveloper d17 = new() { Id = 17, Name = "0verflow" };
        public GameDeveloper d18 = new() { Id = 18, Name = "1-Up Studio" };
        public GameDeveloper d19 = new() { Id = 19, Name = "2K Games" };
        public GameDeveloper d20 = new() { Id = 20, Name = "Elemental Games" };
        public GameDeveloper d21 = new() { Id = 21, Name = "Elite Systems" };
        public GameDeveloper d22 = new() { Id = 22, Name = "Engine Software" };
        public GameDeveloper d23 = new() { Id = 23, Name = "Ensemble Studios" };
        public GameDeveloper d24 = new() { Id = 24, Name = "Epicenter Studios" };
        public GameDeveloper d25 = new() { Id = 25, Name = "Eric Barone" };
        public GameDeveloper d26 = new() { Id = 26, Name = "Epyx" };
        public GameDeveloper d27 = new() { Id = 27, Name = "Ready At Dawn" };
        public GameDeveloper d28 = new() { Id = 28, Name = "Red Entertainment" };
        public GameDeveloper d29 = new() { Id = 29, Name = "Raven Software" };
        public GameDeveloper d30 = new() { Id = 30, Name = "Techland" };
        public GameDeveloper d31 = new() { Id = 31, Name = "Telltale Games" };
        public GameDeveloper d32 = new() { Id = 32, Name = "Nintendo Software Technology" };
        public GameDeveloper d33 = new() { Id = 33, Name = "Nippon Ichi Software" };
        public GameDeveloper d34 = new() { Id = 34, Name = "Demiurge Studios" };
        public GameDeveloper d35 = new() { Id = 35, Name = "DeNA" };
        public GameDeveloper d36 = new() { Id = 36, Name = "DevCAT Studios" };
        public GameDeveloper d37 = new() { Id = 37, Name = "Dhruva Interactive" };
        public GameDeveloper d38 = new() { Id = 38, Name = "Die Gute Fabrik" };
        public GameDeveloper d39 = new() { Id = 39, Name = "Digital Extremes" };    
        public GameDeveloper d40 = new() { Id = 40, Name = "CD Projekt Red" };

        public List<GameDeveloper> ListGameDevelopers;

        public PdGameDevelopers()
        {
            ListGameDevelopers = new()
            {
                d1, d2, d3, d4, d5, d6, d7, d8, d9, d10, d11, d12, d13, d14, d15, d16, d17, d18, d19, d20, d21,
                d22, d23, d24, d25, d26, d27, d28, d29, d30, d31, d32, d33, d34, d35, d36, d37, d38, d39, d40
            };
        }
    }
}
