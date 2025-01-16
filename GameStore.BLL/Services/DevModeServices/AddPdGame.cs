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

        private readonly string defPath_poster = Path.Combine("wwwroot", "img", "gamesposters");
        private readonly string defPath_screenshotGame = Path.Combine("wwwroot", "img", "gamesposters", "screenshotGame");

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

            Game g6 = await Game6();
            games.Add(g6);

            Game g7 = await Game7();
            games.Add(g7);

            Game g8 = await Game8();
            games.Add(g8);

            Game g9 = await Game9();
            games.Add(g9);

            Game g10 = await Game10();
            games.Add(g10);

            Game g11 = await Game11();
            games.Add(g11);

            Game g12 = await Game12();
            games.Add(g12);

            Game g13 = await Game13();
            games.Add(g13);

            Game g14 = await Game14();
            games.Add(g14);

            Game g15 = await Game15();
            games.Add(g15);

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

        private async Task<Game> Game6()
        {
            PredefinedManager pd = new();

            Game g = new() { Name = "Borderlands 3" };
            g.Description = "Всеми любимый «шутер с базиллионами лута» возвращается, чтобы порадовать вас несметным множеством убойных стволов и новым крышесносным приключением! Вам предстоит покорить доселе не виданные миры, играя за одного из четырёх новых искателей Хранилища – нереально крутых перцев, у каждого из которых уникальные навыки, способности и модификации. Действуя в одиночку или в компании друзей, вы должны будете дать бой яростным противникам, нагрести побольше трофеев и спасти свой дом от безжалостных психопатов, возглавляющих самую опасную секту в галактике.";
            g.GameGanres = await GetSelectedGanre([pd.Genres.action.Id, pd.Genres.adventure.Id, pd.Genres.rpg.Id, pd.Genres.openWorld.Id]);
            g.DeveloperId = pd.GameDevelopers.d19.Id;
            g.GameLabels = await GetSelectedLabels([pd.GameLabels.pve.Id, pd.GameLabels.coop.Id]);
            g.ReleaseDate = new DateTime(2019, 09, 13).ToUniversalTime();
            g.Price = 77;
            g.IsShare = false;
            g.SharePrice = null;
            g.YtLinkGameTrailer = "tQj8CLKoTCs";
            g.Os = "Windows 7/10 (с последней версией пакета обновлений)";
            g.Cpu = "AMD Ryzen™ 5 2600 (Intel i7-4770)";
            g.Gpu = "AMD Radeon™ RX 590 (NVIDIA GeForce GTX 1060 6 ГБ)";
            g.Ram = "16гб";
            g.Weight = 75;
            g.IsDeleted = false;
            g.IsVisible = true;
            g.DateAddedSite = DateTime.UtcNow;

            //Медиа файлы
            g.PosterName = "Borderlands3.png";
            g.Poster = UploadPoster(Path.Combine(defPath_poster, "Borderlands3.png").ToString());
            g.Screenshots = UploadGameScreenshot([
                Path.Combine(defPath_screenshotGame, "borderlands3_screenshot_1.png").ToString(),
                Path.Combine(defPath_screenshotGame, "borderlands3_screenshot_2.png").ToString(),
                Path.Combine(defPath_screenshotGame, "borderlands3_screenshot_3.png").ToString(),
                Path.Combine(defPath_screenshotGame, "borderlands3_screenshot_4.png").ToString()
                ]);
            g.GameKeys = UploadGameKeys(23);
            return g;
        }

        private async Task<Game> Game7()
        {
            PredefinedManager pd = new();

            Game g = new() { Name = "Ведьмак 3: Дикая Охота" };
            g.Description = "Станьте профессиональным убийцей монстров и отправляйтесь в приключение эпических масштабов! После своего выхода игра «Ведьмак 3: Дикая Охота» (The Witcher 3: Wild Hunt) стала настоящей классикой, получив более 250 наград в номинации «Игра года». Вас ждёт более 100 часов грандиозного приключения по открытому миру, а также сюжетные расширения, которые растянутся ещё на 50 часов игры. Это издание включает в себя весь дополнительный контент: новое оружие, броню, экипировку для компаньонов, новый режим игры и побочные квесты.";
            g.GameGanres = await GetSelectedGanre([pd.Genres.action.Id, pd.Genres.adventure.Id, pd.Genres.rpg.Id, pd.Genres.openWorld.Id]);
            g.DeveloperId = pd.GameDevelopers.d40.Id;
            g.GameLabels = await GetSelectedLabels([pd.GameLabels.single.Id]);
            g.ReleaseDate = new DateTime(2014, 05, 20).ToUniversalTime();
            g.Price = 65;
            g.IsShare = false;
            g.SharePrice = null;
            g.YtLinkGameTrailer = "4ndIeNusRLI";
            g.Os = "64-разрядная Windows 7, 64-разрядная Windows 8 (8.1) или 64-разрядная Windows 10";
            g.Cpu = "Intel Core i7 3770 с частотой 3,4 ГГц / AMD FX-8350 с частотой 4 ГГц";
            g.Gpu = "NVIDIA GPU GeForce GTX 770 / AMD GPU Radeon R9 290";
            g.Ram = "8гб";
            g.Weight = 77;
            g.IsDeleted = false;
            g.IsVisible = true;
            g.DateAddedSite = DateTime.UtcNow;

            //Медиа файлы
            g.PosterName = "Witcher3.png";
            g.Poster = UploadPoster(Path.Combine(defPath_poster, "Witcher3.png").ToString());
            g.Screenshots = UploadGameScreenshot([
                Path.Combine(defPath_screenshotGame, "witcher3__screenshot_1.png").ToString(),
                Path.Combine(defPath_screenshotGame, "witcher3__screenshot_2.png").ToString(),
                Path.Combine(defPath_screenshotGame, "witcher3__screenshot_3.png").ToString(),
                Path.Combine(defPath_screenshotGame, "witcher3__screenshot_4.png").ToString()
                ]);
            g.GameKeys = UploadGameKeys(66);
            return g;
        }

        private async Task<Game> Game8()
        {
            PredefinedManager pd = new();

            Game g = new() { Name = "Red Dead Redemption 2" };
            g.Description = "Америка, 1899 год. Артур Морган и другие подручные Датча ван дер Линде вынуждены пуститься в бега. Их банде предстоит участвовать в кражах, грабежах и перестрелках в самом сердце Америки. За ними по пятам идут федеральные агенты и лучшие в стране охотники за головами, а саму банду разрывают внутренние противоречия. Артуру предстоит выбрать, что для него важнее: его собственные идеалы или же верность людям, которые его взрастили.";
            g.GameGanres = await GetSelectedGanre([pd.Genres.action.Id, pd.Genres.rpg.Id, pd.Genres.openWorld.Id]);
            g.DeveloperId = pd.GameDevelopers.d3.Id;
            g.GameLabels = await GetSelectedLabels([pd.GameLabels.single.Id]);
            g.ReleaseDate = new DateTime(2019, 11, 05).ToUniversalTime();
            g.Price = 78;
            g.IsShare = false;
            g.SharePrice = null;
            g.YtLinkGameTrailer = "0kqEBOZaP94";
            g.Os = "Windows 10 - April 2018 Update (v1803)";
            g.Cpu = "Intel® Core™ i7-4770K / AMD Ryzen 5 1500x";
            g.Gpu = "Nvidia GeForce GTX 1060 6 ГБ / AMD Radeon RX 480 4 ГБ";
            g.Ram = "12гб";
            g.Weight = 150;
            g.IsDeleted = false;
            g.IsVisible = true;
            g.DateAddedSite = DateTime.UtcNow;

            //Медиа файлы
            g.PosterName = "rdr2.png";
            g.Poster = UploadPoster(Path.Combine(defPath_poster, "rdr2.png").ToString());
            g.Screenshots = UploadGameScreenshot([
                Path.Combine(defPath_screenshotGame, "RDR2_screenshot_1.png").ToString(),
                Path.Combine(defPath_screenshotGame, "RDR2_screenshot_2.png").ToString(),
                Path.Combine(defPath_screenshotGame, "RDR2_screenshot_3.png").ToString(),
                Path.Combine(defPath_screenshotGame, "RDR2_screenshot_4.png").ToString()
                ]);
            g.GameKeys = UploadGameKeys(53);
            return g;
        }

        private async Task<Game> Game9()
        {
            PredefinedManager pd = new();

            Game g = new() { Name = "Tom Clancy's The Division®2" };
            g.Description = "Вашингтон на грани катастрофы. Нашему обществу угрожает беззаконие и нестабильность, и слухи о перевороте в Капитолии только способствуют хаосу. Мы крайне нуждаемся в каждом действующем агенте группы Division — только с ними мы сможем спасти город, пока не поздно.";
            g.GameGanres = await GetSelectedGanre([pd.Genres.action.Id, pd.Genres.rpg.Id, pd.Genres.openWorld.Id]);
            g.DeveloperId = pd.GameDevelopers.d4.Id;
            g.GameLabels = await GetSelectedLabels([pd.GameLabels.pve.Id, pd.GameLabels.pvp.Id, pd.GameLabels.coop.Id]);
            g.ReleaseDate = new DateTime(2019, 03, 15).ToUniversalTime();
            g.Price = 53;
            g.IsShare = false;
            g.SharePrice = null;
            g.YtLinkGameTrailer = "ssyC-QwcPug";
            g.Os = "Windows 7/8/10";
            g.Cpu = "AMD Ryzen 5 1500X | Intel Core I7-4790";
            g.Gpu = "AMD RX 480 NVIDIA GeForce GTX 970";
            g.Ram = "8гб";
            g.Weight = 44;
            g.IsDeleted = false;
            g.IsVisible = true;
            g.DateAddedSite = DateTime.UtcNow;

            //Медиа файлы
            g.PosterName = "Division2.png";
            g.Poster = UploadPoster(Path.Combine(defPath_poster, "Division2.png").ToString());
            g.Screenshots = UploadGameScreenshot([
                Path.Combine(defPath_screenshotGame, "thedivision2_screenshot_1.png").ToString(),
                Path.Combine(defPath_screenshotGame, "thedivision2_screenshot_2.png").ToString(),
                Path.Combine(defPath_screenshotGame, "thedivision2_screenshot_3.png").ToString(),
                Path.Combine(defPath_screenshotGame, "thedivision2_screenshot_4.png").ToString()
                ]);
            g.GameKeys = UploadGameKeys(23);
            return g;
        }

        private async Task<Game> Game10()
        {
            PredefinedManager pd = new();

            Game g = new() { Name = "Fallout 4" };
            g.Description = "Добро пожаловать в игру нового поколения с открытым миром! Вы — единственный выживший из убежища 111, оказавшийся в мире, разрушенном ядерной войной. Каждый миг вы сражаетесь за выживание, каждое решение может стать последним. Но именно от вас зависит судьба пустошей. Добро пожаловать домой.";
            g.GameGanres = await GetSelectedGanre([pd.Genres.action.Id, pd.Genres.rpg.Id, pd.Genres.openWorld.Id]);
            g.DeveloperId = pd.GameDevelopers.d12.Id;
            g.GameLabels = await GetSelectedLabels([pd.GameLabels.single.Id]);
            g.ReleaseDate = new DateTime(2015, 11, 10).ToUniversalTime();
            g.Price = 46;
            g.IsShare = false;
            g.SharePrice = null;
            g.YtLinkGameTrailer = "ErgtR14-MV8";
            g.Os = "Windows 7/8/10/11 (необходима 64-битная ОС)";
            g.Cpu = "Intel Core i5-2300 2,8 ГГц/AMD Phenom II X4 945 3 ГГц";
            g.Gpu = "NVIDIA GTX 550 Ti 2 Гб/AMD Radeon HD 7870 2 Гб";
            g.Ram = "8гб";
            g.Weight = 60;
            g.IsDeleted = false;
            g.IsVisible = true;
            g.DateAddedSite = DateTime.UtcNow;

            //Медиа файлы
            g.PosterName = "Fallout4.png";
            g.Poster = UploadPoster(Path.Combine(defPath_poster, "Fallout4.png").ToString());
            g.Screenshots = UploadGameScreenshot([
                Path.Combine(defPath_screenshotGame, "fallout4_1.png").ToString(),
                Path.Combine(defPath_screenshotGame, "fallout4_2.png").ToString(),
                Path.Combine(defPath_screenshotGame, "fallout4_3.png").ToString(),
                Path.Combine(defPath_screenshotGame, "fallout4_4.png").ToString()
                ]);
            g.GameKeys = UploadGameKeys(16);
            return g;
        }

        private async Task<Game> Game11()
        {
            PredefinedManager pd = new();

            Game g = new() { Name = "The Elder Scrolls V: Skyrim" };
            g.Description = "Эта часть, как и предыдущие три игры серии — The Elder Scrolls II: Daggerfall, The Elder Scrolls III: Morrowind и The Elder Scrolls IV: Oblivion — получила титул «Игра года» на Video Game Award 2011. Действие происходит через 200 лет после событий Oblivion. Игра была анонсирована 11 ноября 2010 года, а выпущена 11 ноября 2011 года. В игре задействован новый движок Creation Engine и обновлённая система мелких квестов Radiant Story.";
            g.GameGanres = await GetSelectedGanre([pd.Genres.action.Id, pd.Genres.rpg.Id, pd.Genres.openWorld.Id]);
            g.DeveloperId = pd.GameDevelopers.d12.Id;
            g.GameLabels = await GetSelectedLabels([pd.GameLabels.single.Id]);
            g.ReleaseDate = new DateTime(2011, 11, 11).ToUniversalTime();
            g.Price = 40;
            g.IsShare = false;
            g.SharePrice = null;
            g.YtLinkGameTrailer = "JSRtYpNRoN0";
            g.Os = "Windows 7/8.1/10 (64-разрядные версии)";
            g.Cpu = "Intel i5-2400 или AMD FX-8320";
            g.Gpu = "NVIDIA GTX 780 (3 ГБ) AMD R9 290 (4 ГБ)";
            g.Ram = "8гб";
            g.Weight = 37;
            g.IsDeleted = false;
            g.IsVisible = true;
            g.DateAddedSite = DateTime.UtcNow;

            //Медиа файлы
            g.PosterName = "The_Elder_Scrolls_V_Skyrim.png";
            g.Poster = UploadPoster(Path.Combine(defPath_poster, "The_Elder_Scrolls_V_Skyrim.png").ToString());
            g.Screenshots = UploadGameScreenshot([
                Path.Combine(defPath_screenshotGame, "Skyrim_1.png").ToString(),
                Path.Combine(defPath_screenshotGame, "Skyrim_2.png").ToString(),
                Path.Combine(defPath_screenshotGame, "Skyrim_3.png").ToString(),
                Path.Combine(defPath_screenshotGame, "Skyrim_4.png").ToString()
                ]);
            g.GameKeys = UploadGameKeys(26);
            return g;
        }

        private async Task<Game> Game12()
        {
            PredefinedManager pd = new();

            Game g = new() { Name = "Far Cry® 6" };
            g.Description = "Добро пожаловать в Яру – тропический рай, где время словно остановилось. Диктатор Антон Кастильо намерен любой ценой вернуть былое величие нации. За ним следует его сын – Диего. Но в угнетенной стране вспыхнуло пламя революции.";
            g.GameGanres = await GetSelectedGanre([pd.Genres.action.Id, pd.Genres.rpg.Id, pd.Genres.openWorld.Id]);
            g.DeveloperId = pd.GameDevelopers.d4.Id;
            g.GameLabels = await GetSelectedLabels([pd.GameLabels.single.Id, pd.GameLabels.coop.Id]);
            g.ReleaseDate = new DateTime(2021, 10, 07).ToUniversalTime();
            g.Price = 40;
            g.IsShare = false;
            g.SharePrice = null;
            g.YtLinkGameTrailer = "YqDpa6gHAZg";
            g.Os = "Windows 10 (версия 20H1 или новее) — только 64-разрядная версия";
            g.Cpu = "AMD Ryzen 5 3600X с частотой 3,8 ГГц / Intel i7-7700 с частотой 3,6 ГГц или выше";
            g.Gpu = "AMD RX VEGA 64 (8 ГБ) / NVIDIA GTX 1080 (8 ГБ) или выше";
            g.Ram = "16гб";
            g.Weight = 69;
            g.IsDeleted = false;
            g.IsVisible = true;
            g.DateAddedSite = DateTime.UtcNow;

            //Медиа файлы
            g.PosterName = "FarCry6_poster.png";
            g.Poster = UploadPoster(Path.Combine(defPath_poster, "FarCry6_poster.png").ToString());
            g.Screenshots = UploadGameScreenshot([
                Path.Combine(defPath_screenshotGame, "FarCry6_1.png").ToString(),
                Path.Combine(defPath_screenshotGame, "FarCry6_2.png").ToString(),
                Path.Combine(defPath_screenshotGame, "FarCry6_3.png").ToString(),
                Path.Combine(defPath_screenshotGame, "FarCry6_4.png").ToString()
                ]);
            g.GameKeys = UploadGameKeys(44);
            return g;
        }

        private async Task<Game> Game13()
        {
            PredefinedManager pd = new();

            Game g = new() { Name = "Far Cry® 5" };
            g.Description = "Добро пожаловать в округ Хоуп, штат Монтана, земли свободолюбцев и храбрецов, где всем заправляет секта конца света «Врата Эдема».Освободите округ Хоуп в одиночку или вместе с другим игроком. Пользуйтесь услугами наёмников и приручайте животных, чтобы уничтожить секту.";
            g.GameGanres = await GetSelectedGanre([pd.Genres.action.Id, pd.Genres.rpg.Id, pd.Genres.openWorld.Id]);
            g.DeveloperId = pd.GameDevelopers.d4.Id;
            g.GameLabels = await GetSelectedLabels([pd.GameLabels.single.Id, pd.GameLabels.coop.Id]);
            g.ReleaseDate = new DateTime(2018, 03, 27).ToUniversalTime();
            g.Price = 35;
            g.IsShare = false;
            g.SharePrice = null;
            g.YtLinkGameTrailer = "ZCBC2kN33jw";
            g.Os = "Windows 7 SP1 | Windows 8.1 | Windows 10 | Только 64-разрядные версии";
            g.Cpu = "Intel Core i7-4770 с частотой 3,4 ГГц | AMD Ryzen 5 1600 с частотой 3,2 ГГц или эквивалентный";
            g.Gpu = "NVIDIA GeForce GTX 970 | AMD R9 290X";
            g.Ram = "8гб";
            g.Weight = 49;
            g.IsDeleted = false;
            g.IsVisible = true;
            g.DateAddedSite = DateTime.UtcNow;

            //Медиа файлы
            g.PosterName = "Far_Cry_5.png";
            g.Poster = UploadPoster(Path.Combine(defPath_poster, "Far_Cry_5.png").ToString());
            g.Screenshots = UploadGameScreenshot([
                Path.Combine(defPath_screenshotGame, "FarCry5_1.png").ToString(),
                Path.Combine(defPath_screenshotGame, "FarCry5_2.png").ToString(),
                Path.Combine(defPath_screenshotGame, "FarCry5_3.png").ToString(),
                Path.Combine(defPath_screenshotGame, "FarCry5_4.png").ToString()
                ]);
            g.GameKeys = UploadGameKeys(34);
            return g;
        }

        private async Task<Game> Game14()
        {
            PredefinedManager pd = new();

            Game g = new() { Name = "Assassin's Creed Origins" };
            g.Description = "Раскройте темные тайны и забытые мифы, возвращаясь к моменту основания: Истоки Братства ассасинов. Испытайте совершенно новый способ ведения боя. Добывайте и используйте десятки видов оружия с различными характеристиками и редкостями.Плывите вниз по Нилу, раскройте тайны пирамид или сражайтесь с опасными древними группировками и дикими зверями, исследуя эту гигантскую и непредсказуемую страну.";
            g.GameGanres = await GetSelectedGanre([pd.Genres.action.Id, pd.Genres.rpg.Id, pd.Genres.openWorld.Id]);
            g.DeveloperId = pd.GameDevelopers.d4.Id;
            g.GameLabels = await GetSelectedLabels([pd.GameLabels.single.Id]);
            g.ReleaseDate = new DateTime(2017, 10, 26).ToUniversalTime();
            g.Price = 44;
            g.IsShare = false;
            g.SharePrice = null;
            g.YtLinkGameTrailer = "x_1GwlWSil4";
            g.Os = "Windows 7 SP1 | Windows 8.1 | Windows 10 | Только 64-разрядные версии";
            g.Cpu = "Intel Core i5-2400s с частотой 2,5 ГГц | AMD FX-6350 с частотой 3,9 ГГц или эквивалент";
            g.Gpu = "NVIDIA GeForce GTX 760 или AMD R9 270/2048 МБ видеопамяти с шейдерной моделью 5.0 или выше";
            g.Ram = "8гб";
            g.Weight = 42;
            g.IsDeleted = false;
            g.IsVisible = true;
            g.DateAddedSite = DateTime.UtcNow;

            //Медиа файлы
            g.PosterName = "Assassin_Origins.png";
            g.Poster = UploadPoster(Path.Combine(defPath_poster, "Assassin_Origins.png").ToString());
            g.Screenshots = UploadGameScreenshot([
                Path.Combine(defPath_screenshotGame, "Assassin_Origins_1.png").ToString(),
                Path.Combine(defPath_screenshotGame, "Assassin_Origins_2.png").ToString(),
                Path.Combine(defPath_screenshotGame, "Assassin_Origins_3.png").ToString(),
                Path.Combine(defPath_screenshotGame, "Assassin_Origins_4.png").ToString()
                ]);
            g.GameKeys = UploadGameKeys(30);
            return g;
        }

        private async Task<Game> Game15()
        {
            PredefinedManager pd = new();

            Game g = new() { Name = "Assassin's Creed Odyssey" };
            g.Description = "Определите свою судьбу в игре Assassin's Creed® Одиссея. Пройдите путь от изгоя до живой легенды: отправьтесь в далёкое странствие, чтобы раскрыть тайны своего прошлого и изменить будущее Древней Греции. Путешествуйте по густым зелёным лесам, вулканическим островам и шумным городам — встаньте на путь открытий и встреч в мире, что погряз в войне, начатой богами и людьми.";
            g.GameGanres = await GetSelectedGanre([pd.Genres.action.Id, pd.Genres.rpg.Id, pd.Genres.openWorld.Id]);
            g.DeveloperId = pd.GameDevelopers.d4.Id;
            g.GameLabels = await GetSelectedLabels([pd.GameLabels.single.Id]);
            g.ReleaseDate = new DateTime(2018, 10, 05).ToUniversalTime();
            g.Price = 54;
            g.IsShare = false;
            g.SharePrice = null;
            g.YtLinkGameTrailer = "x_1GwlWSil4";
            g.Os = "Windows 7 SP1 | Windows 8.1 | Windows 10 | Только 64-разрядные версии";
            g.Cpu = "Не ниже AMD FX-8350 с частотой 4 ГГц | Ryzen 5 — 1400 | Intel Core i7-3770 с частотой 3,5 ГГц";
            g.Gpu = "AMD Radeon R9 285 | NVIDIA GeForce GTX 660 | 2 ГБ видеопамяти и Shader Model 5.0";
            g.Ram = "8гб";
            g.Weight = 42;
            g.IsDeleted = false;
            g.IsVisible = true;
            g.DateAddedSite = DateTime.UtcNow;

            //Медиа файлы
            g.PosterName = "Assassins_Creed_Odyssey.png";
            g.Poster = UploadPoster(Path.Combine(defPath_poster, "Assassins_Creed_Odyssey.png").ToString());
            g.Screenshots = UploadGameScreenshot([
                Path.Combine(defPath_screenshotGame, "Assassins_Creed_Odyssey_1.png").ToString(),
                Path.Combine(defPath_screenshotGame, "Assassins_Creed_Odyssey_2.png").ToString(),
                Path.Combine(defPath_screenshotGame, "Assassins_Creed_Odyssey_3.png").ToString(),
                Path.Combine(defPath_screenshotGame, "Assassins_Creed_Odyssey_4.png").ToString()
                ]);
            g.GameKeys = UploadGameKeys(23);
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
