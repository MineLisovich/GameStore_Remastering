using GameStore.BLL.Infrastrcture;
using GameStore.BLL.Infrastrcture.Singletons;
using GameStore.BLL.Predefined;
using GameStore.DAL.Domain;
using GameStore.DAL.Entities.Dictionaries;
using GameStore.DAL.Entities.Games;
using Microsoft.EntityFrameworkCore;

namespace GameStore.BLL.Services.DevModeServices
{
    public class AddPdGame : IAddPdGame
    {
        private readonly GsDbContext _dbContext;
        public AddPdGame(GsDbContext gsDbContext)
        {
            _dbContext = gsDbContext;
        }
        public async Task<ResultServiceModel> AddPdGames()
        {
            ResultServiceModel result = new();

            List<Game> games = new();

            Game g1 = await Game1();
            games.Add(g1);

            Game g2 = await Game2();
            games.Add(g2);

            Game g3 = await Game3();
            games.Add(g3);

            Game g4 = await Game4();
            games.Add(g4);

            Game g5 = await Game5();
            games.Add(g5);

            try
            {
                _dbContext.Games.UpdateRange(games);
                await _dbContext.SaveChangesAsync();
            }
            catch { result.IsSucceeded = false; result.ErrorMes = DefaultErrorMessages.dontSave; return result; }


            result.IsSucceeded = true;
            return result;
        }

        #region Игры
        private async Task<Game> Game1()
        {
            PredefinedManager pd = new();
            string defPath_poster = Path.Combine("wwwroot", "img", "gamesposters");
            string defPath_screenshotGame = Path.Combine("wwwroot", "img", "gamesposters", "screenshotGame");

            Game g = new() { Name = "Cyberpunk 2077" };
            g.Description = "Cyberpunk 2077 — приключенческая ролевая игра, действие которой происходит в мегаполисе Найт-Сити, где власть, роскошь и модификации тела ценятся выше всего. Вы играете за V, наёмника в поисках устройства, позволяющего обрести бессмертие. Вы сможете менять киберимпланты, навыки и стиль игры своего персонажа, исследуя открытый мир, где ваши поступки влияют на ход сюжета и всё, что вас окружает.";
            g.GameGanres = await GetSelectedGanre([pd.Genres.action.Id, pd.Genres.rpg.Id, pd.Genres.openWorld.Id]);
            g.DeveloperId = pd.GameDevelopers.d40.Id;
            g.GameLabels = await GetSelectedLabels([pd.GameLabels.pve.Id, pd.GameLabels.single.Id]);
            g.ReleaseDate = new DateTime(2020, 12, 10).ToUniversalTime();
            g.Price = 60;
            g.IsShare = false;
            g.SharePrice = null;
            g.YtLinkGameTrailer = "aSrFWinrkeQ";
            g.Os = "Windows 10 64 bit";
            g.Cpu = "Intel Core i5-11400F or AMD Ryzen 5 5600X";
            g.Gpu = "Nvidia GeForce GTX 1660 GP 6GB GDDR6";
            g.Ram = "8гб";
            g.Weight = 80;
            g.IsDeleted = false;
            g.IsVisible = true;
            g.DateAddedSite = DateTime.UtcNow;

            //Медиа файлы
            g.PosterName = "2077.png";
            g.Poster = UploadPoster(Path.Combine(defPath_poster, "2077.png").ToString());
            g.Screenshots = UploadGameScreenshot([
                Path.Combine(defPath_screenshotGame, "Cyberpunk_screenshotGame_1.png").ToString(),
                Path.Combine(defPath_screenshotGame, "Cyberpunk_screenshotGame_2.png").ToString(),
                Path.Combine(defPath_screenshotGame, "Cyberpunk_screenshotGame_3.png").ToString(),
                Path.Combine(defPath_screenshotGame, "Cyberpunk_screenshotGame_4.png").ToString()
                ]);
            g.GameKeys = UploadGameKeys(10);
            return g;
        }

        private async Task<Game> Game2()
        {
            PredefinedManager pd = new();
            string defPath_poster = Path.Combine("wwwroot", "img", "gamesposters");
            string defPath_screenshotGame = Path.Combine("wwwroot", "img", "gamesposters", "screenshotGame");

            Game g = new() { Name = "Grand Theft Auto V" };
            g.Description = "Действие игры происходит в вымышленном штате Сан-Андреас, прообразом которого послужила Южная Калифорния. Сюжет в однопользовательском режиме строится вокруг приключений троих грабителей, устраивающих всё более дерзкие ограбления и противостоящих как организованной преступности, так и правоохранительным ведомствам. В процессе игры игрок управляет выбранным персонажем в режиме от первого или от третьего лица; персонаж может свободно передвигаться по обширному миру игры как пешком, так и на автомобилях и других видах транспорта. Особенностью Grand Theft Auto V по сравнению с другими играми серии является возможность переключаться между персонажами в любой момент, как во время выполнения заданий, так и вне их. Многие задания игры связаны с ограблениями и угоном автомобилей; при этом игровой персонаж может участвовать в перестрелках и погонях. Grand Theft Auto Online представляет собой встроенный многопользовательский онлайн-режим, поддерживающий до 30 игроков одновременно — для них предлагаются как кооперативные, так и соревновательные задания.";
            g.GameGanres = await GetSelectedGanre([pd.Genres.action.Id, pd.Genres.rpg.Id, pd.Genres.openWorld.Id]);
            g.DeveloperId = pd.GameDevelopers.d3.Id;
            g.GameLabels = await GetSelectedLabels([pd.GameLabels.pve.Id, pd.GameLabels.single.Id]);
            g.ReleaseDate =  new DateTime(2013, 12, 17).ToUniversalTime();
            g.Price = 40;
            g.IsShare = false;
            g.SharePrice = null;
            g.YtLinkGameTrailer = "QkkoHAzjnUs";
            g.Os = "Windows 7 64 bit / Windows 10 64 bit";
            g.Cpu = "Intel® Core™ 2 Q6600 / AMD Phenom 9850";
            g.Gpu = "NVIDIA® 9800 GT / AMD HD 4870";
            g.Ram = "4гб";
            g.Weight = 120;
            g.IsDeleted = false;
            g.IsVisible = true;
            g.DateAddedSite = DateTime.UtcNow;

            //Медиа файлы
            g.PosterName = "GTA5.png";
            g.Poster = UploadPoster(Path.Combine(defPath_poster, "GTA5.png").ToString());
            g.Screenshots = UploadGameScreenshot([
                Path.Combine(defPath_screenshotGame, "GTAV_screenshotGame_1.png").ToString(),
                Path.Combine(defPath_screenshotGame, "GTAV_screenshotGame_2.png").ToString(),
                Path.Combine(defPath_screenshotGame, "GTAV_screenshotGame_3.png").ToString(),
                Path.Combine(defPath_screenshotGame, "GTAV_screenshotGame_4.png").ToString()
                ]);
            g.GameKeys = UploadGameKeys(15);
            return g;
        }

        private async Task<Game> Game3()
        {
            PredefinedManager pd = new();
            string defPath_poster = Path.Combine("wwwroot", "img", "gamesposters");
            string defPath_screenshotGame = Path.Combine("wwwroot", "img", "gamesposters", "screenshotGame");

            Game g = new() { Name = "Valheim" };
            g.Description = "Вальхейм — это игра, в которой вам предстоит исследовать огромный фэнтезийный мир, пропитанный скандинавской мифологией и культурой викингов. Ваше приключение начнется в самом сердце Вальхейма, месте довольно спокойном. Но берегитесь, ведь чем дальше вы будете продвигаться, тем опаснее будет становиться мир вокруг. К счастью, по пути вас будут ждать не только опасности — вы также будете чаще находить ценные материалы, которые весьма пригодятся для создания смертоносного оружия и крепкой брони. Возводите крепости и заставы по всему миру! А со временем постройте несокрушимый драккар и отправьтесь покорять бескрайние океаны в поиске чужестранных земель... Но постарайтесь не заплыть слишком далеко...";
            g.GameGanres = await GetSelectedGanre([pd.Genres.rpg.Id, pd.Genres.openWorld.Id]);
            g.DeveloperId = pd.GameDevelopers.d9.Id;
            g.GameLabels = await GetSelectedLabels([pd.GameLabels.pve.Id, pd.GameLabels.coop.Id]);
            g.ReleaseDate = new DateTime(2021, 02, 02).ToUniversalTime();
            g.Price = 25;
            g.IsShare = false;
            g.SharePrice = null;
            g.YtLinkGameTrailer = "5mHRJ1KFe20";
            g.Os = "Windows 7 64 bit";
            g.Cpu = "2.6 GHz Quad Core or similar";
            g.Gpu = "GeForce GTX 950 or Radeon HD 7970";
            g.Ram = "8гб";
            g.Weight = 2;
            g.IsDeleted = false;
            g.IsVisible = true;
            g.DateAddedSite = DateTime.UtcNow;

            //Медиа файлы
            g.PosterName = "Valheim.png";
            g.Poster = UploadPoster(Path.Combine(defPath_poster, "Valheim.png").ToString());
            g.Screenshots = UploadGameScreenshot([
                Path.Combine(defPath_screenshotGame, "Valheim_screenshotGame_1.png").ToString(),
                Path.Combine(defPath_screenshotGame, "Valheim_screenshotGame_2.png").ToString(),
                Path.Combine(defPath_screenshotGame, "Valheim_screenshotGame_3.png").ToString(),
                Path.Combine(defPath_screenshotGame, "Valheim_screenshotGame_4.png").ToString()
                ]);
            g.GameKeys = UploadGameKeys(44);
            return g;
        }

        private async Task<Game> Game4()
        {
            PredefinedManager pd = new();
            string defPath_poster = Path.Combine("wwwroot", "img", "gamesposters");
            string defPath_screenshotGame = Path.Combine("wwwroot", "img", "gamesposters", "screenshotGame");

            Game g = new() { Name = "Assassin’s Creed" };
            g.Description = "Действие игры разворачивается во времена Третьего крестового похода, а именно в 1191 году. В настоящем времени бармена Дезмонда Майлса, главного героя, похищает корпорация «Абстерго», которая с помощью Анимуса, машины для извлечения генетической памяти, хочет найти артефакт Первой Цивилизации. В курс дела Дезмонда вводят учёный Уоррен Видик и его ассистентка Люси Стиллман. Они рассказывают ему, что он является потомком ассасина Альтаира ибн-Ла-Ахада, который обнаружил артефакт, и через него хотят узнать местонахождение артефакта.";
            g.GameGanres = await GetSelectedGanre([pd.Genres.action.Id, pd.Genres.openWorld.Id]);
            g.DeveloperId = pd.GameDevelopers.d4.Id;
            g.GameLabels = await GetSelectedLabels([pd.GameLabels.single.Id]);
            g.ReleaseDate = new DateTime(2007, 11, 13).ToUniversalTime();
            g.Price = 10;
            g.IsShare = false;
            g.SharePrice = null;
            g.YtLinkGameTrailer = "RjQ6ZtyXoA0";
            g.Os = "Windows XP/Vista / Windows 7";
            g.Cpu = "Dual core (Intel Pentium D или лучше)";
            g.Gpu = "256MB с поддержкой Shader Model 3.0 или выше";
            g.Ram = "2гб";
            g.Weight = 16;
            g.IsDeleted = false;
            g.IsVisible = true;
            g.DateAddedSite = DateTime.UtcNow;

            //Медиа файлы
            g.PosterName = "Assassin1.png";
            g.Poster = UploadPoster(Path.Combine(defPath_poster, "Assassin1.png").ToString());
            g.Screenshots = UploadGameScreenshot([
                Path.Combine(defPath_screenshotGame, "Assassin1_screenshotGame_1.png").ToString(),
                Path.Combine(defPath_screenshotGame, "Assassin1_screenshotGame_2.png").ToString(),
                Path.Combine(defPath_screenshotGame, "Assassin1_screenshotGame_3.png").ToString(),
                Path.Combine(defPath_screenshotGame, "Assassin1_screenshotGame_4.png").ToString()
                ]);
            g.GameKeys = UploadGameKeys(17);
            return g;
        }

        private async Task<Game> Game5()
        {
            PredefinedManager pd = new();
            string defPath_poster = Path.Combine("wwwroot", "img", "gamesposters");
            string defPath_screenshotGame = Path.Combine("wwwroot", "img", "gamesposters", "screenshotGame");

            Game g = new() { Name = "Fallout 76" };
            g.Description = "События игры происходят в 2102 году в Западной Виргинии. Игрок — житель Убежища 76 (Резидент), проспавший выход на поверхность. Находя голозаписи Смотрительницы Убежища, которая покинула его раньше всех, игрок понимает, что над регионом нависла опасность в виде горелых — людей, заражённых инфекцией, превращающихся со временем в неподвижные статуи, которые, распадаясь, разносят заразу, заражая как и других существ, так и людей. Как выясняется, источник той болезни — зверожоги. Это мутировавшие драконоподобные летучие мыши, обитавшие под землёй. По мере продвижения по сюжету и выполнению квестов игрок создаёт вакцину против чумы зверожогов. Далее Резиденту предстоит проникнуть в хорошо спрятанный бункер «Анклава» — бывшего правительства США. Там ему встречается МОДУС — суперкомпьютер, который убил всех членов Анклава в качестве мести за попытку уничтожить его. МОДУС рассказывает о ядерных ракетах и как их запустить. Игрок завладевает кодами запуска и, проведя бомбардировку главного разлома, откуда вылезают зверожоги, сталкивается с ещё более страшной угрозой — маткой зверожогов. В тяжёлом бою её удаётся убить, и зверожоги, оставшись без главы, разлетаются подобно муравьям, оставшимся без королевы.";
            g.GameGanres = await GetSelectedGanre([pd.Genres.action.Id, pd.Genres.adventure.Id, pd.Genres.rpg.Id, pd.Genres.openWorld.Id]);
            g.DeveloperId = pd.GameDevelopers.d12.Id;
            g.GameLabels = await GetSelectedLabels([pd.GameLabels.pvp.Id, pd.GameLabels.pve.Id, pd.GameLabels.coop.Id]);
            g.ReleaseDate = new DateTime(2018, 10, 23).ToUniversalTime();
            g.Price = 80;
            g.IsShare = false;
            g.SharePrice = null;
            g.YtLinkGameTrailer = "EtiVOmFiWA0";
            g.Os = "Windows 7/8/8.1/10";
            g.Cpu = "Intel Core i5-6600K";
            g.Gpu = "NVIDIA GeForce GTX 780";
            g.Ram = "8гб";
            g.Weight = 60;
            g.IsDeleted = false;
            g.IsVisible = true;
            g.DateAddedSite = DateTime.UtcNow;

            //Медиа файлы
            g.PosterName = "Fallout76.png";
            g.Poster = UploadPoster(Path.Combine(defPath_poster, "Fallout76.png").ToString());
            g.Screenshots = UploadGameScreenshot([
                Path.Combine(defPath_screenshotGame, "Fallout76Screenshot_1.png").ToString(),
                Path.Combine(defPath_screenshotGame, "Fallout76Screenshot_2.png").ToString(),
                Path.Combine(defPath_screenshotGame, "Fallout76Screenshot_3.png").ToString(),
                Path.Combine(defPath_screenshotGame, "Fallout76Screenshot_4.png").ToString()
                ]);
            g.GameKeys = UploadGameKeys(44);
            return g;
        }
        #endregion



        #region ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ
        private async Task<List<Genre>> GetSelectedGanre(int[] ganreIds)
        {
            List<Genre> genres = new();

            if (ganreIds is not null)
            {
                foreach (int ganreId in ganreIds)
                {
                    Genre get = await _dbContext.Genres.Where(x => x.Id == ganreId).FirstOrDefaultAsync();
                    if (get != null)
                    {
                        genres.Add(get);
                    }
                }
            }
            return genres;
        }

        private async Task<List<GameLabel>> GetSelectedLabels(int[] gamesLabelsIds)
        {
            List<GameLabel> gameLabels = new();
            if (gamesLabelsIds is not null)
            {
                foreach (int gmLId in gamesLabelsIds)
                {
                    GameLabel get = await _dbContext.GameLabels.Where(x => x.Id == gmLId).FirstOrDefaultAsync();
                    if (get != null)
                    {
                        gameLabels.Add(get);
                    }
                }
            }
            return gameLabels;
        }

        private byte[] UploadPoster(string posterPath)
        {
            if (string.IsNullOrEmpty(posterPath) || !File.Exists(posterPath)) return null;

            byte[] posterData = null;
            using (var stream = new FileStream(posterPath, FileMode.Open, FileAccess.Read))
            using (var binaryReader = new BinaryReader(stream))
            {
                posterData = binaryReader.ReadBytes((int)stream.Length);
            }
            return posterData;
        }

        private List<GameScreenshot> UploadGameScreenshot(string[] uploadScreens)
        {
            if (uploadScreens == null) return new();

            List<GameScreenshot> screens = new();

            foreach (var screen in uploadScreens)
            {
                if (string.IsNullOrEmpty(screen) || !File.Exists(screen)) return null;


                byte[] scData = null;
                using (var stream = new FileStream(screen, FileMode.Open, FileAccess.Read))
                using (var binaryReader = new BinaryReader(stream))
                {
                    scData = binaryReader.ReadBytes((int)stream.Length);
                }

                GameScreenshot gameSc = new()
                {
                    Screenshot = scData,
                    ScreenshotName = screen,
                };
                screens.Add(gameSc);
            }

            return screens;
        }

        private List<GameKey> UploadGameKeys(int countKyes)
        {
            PredefinedManager pd = new();
            GameKeyCryptography gkc = GameKeyCryptography.GetIstance();
            List<GameKey> keys = new();

            for (int i = 0; i <= countKyes; i++)
            {
                string key = gkc.Encrypt(Guid.NewGuid().ToString());
                keys.Add(new GameKey { PlatformId = 1, Key = key, StatusId = pd.GameKeyStatuses.active.Id });
            }
            return keys;
        }
        #endregion
    }
}
