using GameStore.BLL.DTO.Dictionaries;
using GameStore.BLL.DTO.Games;
using GameStore.BLL.Infrastrcture;
using GameStore.BLL.Predefined;
using GameStore.BLL.Services.HomeServices;
using GameStore.WEB.Infrastrcture;
using GameStore.WEB.Models.HomeModels.HomePageModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;



namespace GameStore.WEB.Controllers
{
  
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService) 
        {
            _homeService = homeService;
        }

        #region PUBLIC METHODS - GET
        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetUserInfoForHeader()
        {
            string userData = await _homeService.GetUserInfoForHeader(User.Identity.Name);
            return Json(userData);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task <IActionResult> Index()
        {
            HomePageModel model = await CreateHomePageModel(TempData);
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPartialWorkOnData (long gameId)
        {
            HomeDataModel model = await CreateHomeDataModel(gameId);
            return PartialView("_Partial.Index.Modal.AddInShCart", model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Support()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task <IActionResult> GamePage(long gameId)
        {
            PredefinedManager pd = new();
            SingleGamePageModel model = await CreateSingleGamePageModel(gameId,TempData);
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetGamePlatforms(long gameId)
        {
            PredefinedManager pd = new();
            SingleGamePageModel model = await CreateSingleGamePageModel(gameId, pd.GamePageParts.part_platforms.OrderId);
            return PartialView("_Partial.GamePage.Plaforms", model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetGameGenres(long gameId)
        {
            PredefinedManager pd = new();
            SingleGamePageModel model = await CreateSingleGamePageModel(gameId, pd.GamePageParts.part_genres.OrderId);
            return PartialView("_Partial.GamePage.Genres", model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetGameLables(long gameId)
        {
            PredefinedManager pd = new();
            SingleGamePageModel model = await CreateSingleGamePageModel(gameId, pd.GamePageParts.part_lables.OrderId);
            return PartialView("_Partial.GamePage.Lables", model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetGameScreens(long gameId)
        {
            PredefinedManager pd = new();
            SingleGamePageModel model = await CreateSingleGamePageModel(gameId, pd.GamePageParts.part_screens.OrderId);
            SliderModel sliderModel = new();
            sliderModel.Screenshots = model.Screenshots;
            sliderModel.IsSingleGamePage = model.IsSingleGamePage;
           
            return PartialView("_Partial.Index.Slider", sliderModel);
        }

        #endregion

        #region PUBLIC METHODS - POST
        #endregion

        #region PRIVATE METHODS - TEMPDATA
        private UserActionResult GetInfoAboutLastActionFromTempData(ITempDataDictionary TempData)
        {
            UserActionResult lastAction = (TempData.ContainsKey("LastAction")) ? JsonConvert.DeserializeObject<UserActionResult>((string)TempData["LastAction"]) : new();
            return lastAction;
        }

        private ITempDataDictionary SetTempDataForInfoAboutLastAction(ResultServiceModel result, int actionTypeId)
        {
            PredefinedManager pd = new();
            StandartUserActionTypes actionTypes = new();
            string mainMessage = "";
            if (actionTypeId == actionTypes.Create.Id)
            {
                mainMessage = (result.IsSucceeded) ? "Игра успешно добавлена в корзину" : "Не удалось добавить игру в корзину";
            }
          

            UserActionResult lastAction = new();
            lastAction.Id = actionTypeId;
            lastAction.IsSuccess = result.IsSucceeded;
            lastAction.DopInfo = mainMessage + ". " + result.ErrorMes;
            TempData["LastAction"] = JsonConvert.SerializeObject(lastAction);
            return TempData;

        }
        #endregion

        #region PRIVATE METHODS
        private async Task<HomePageModel> CreateHomePageModel(ITempDataDictionary TempData)
        {
            PredefinedManager pd = new();
            HomePageModel model = new();
            model.SliderGames = await _homeService.GetGamesWithSelectedTypeAsync(pd.SelectedGames.slider_games.OrderId);
            model.NewGames = await _homeService.GetGamesWithSelectedTypeAsync(pd.SelectedGames.new_games.OrderId);
            model.SalesLeaders = await _homeService.GetGamesWithSelectedTypeAsync(pd.SelectedGames.sales_leaders.OrderId);
            model.ComingSoon = await _homeService.GetGamesWithSelectedTypeAsync(pd.SelectedGames.coming_soon.OrderId);
            model.LastAction = GetInfoAboutLastActionFromTempData(TempData);
            return model;
        }
        
        private async Task<SingleGamePageModel> CreateSingleGamePageModel(long gameId ,ITempDataDictionary TempData)
        {
            SingleGamePageModel model = new();
            model.IsSingleGamePage = true;
            model.Game = await _homeService.GetGameByIdAsync(gameId);
            model.IsCanAddToShoppCart = await _homeService.IsCanAddToShoppingCart(gameId);

            if (model.Game is  null)
            {
                model.ErrorVM.Title = "Ой, что-то пошло не так";
                model.ErrorVM.Message = "Не удалось найти игру. Попробуйте позже. Если проблема не исчезла, сообщите нам о проблеме! Сообщить можно на вкладке Поддержка.";
            }

            model.LastAction = GetInfoAboutLastActionFromTempData(TempData);

            return model;
        }
        
        private async Task<HomeDataModel> CreateHomeDataModel(long gameId)
        {
            HomeDataModel model = new();
            model.Game = await _homeService.GetGameByIdForPartial(gameId);
            List<GamePlatformDTO> platforms = model.Game.GameKeys.DistinctBy(x => x.PlatformId).Select(x => x.Platform).ToList();
            model.SelectItemsPlatforms = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(platforms, "Id", "Name");
            return model;
        }

        private async Task<SingleGamePageModel> CreateSingleGamePageModel(long gameId, int gamePagePartId)
        {
            PredefinedManager pd = new();
            SingleGamePageModel model = new();
            GameDTO game = await _homeService.GetGamePartsAsync(gameId,gamePagePartId);
            
            if(gamePagePartId == pd.GamePageParts.part_platforms.OrderId)
            {
                model.Platforms = game.GameKeys.DistinctBy(x => x.PlatformId).Select(x => x.Platform).ToList();
            }
            else if(gamePagePartId == pd.GamePageParts.part_genres.OrderId)
            {
                model.Genres = game.GameGanres.ToList();
            }
            else if (gamePagePartId == pd.GamePageParts.part_lables.OrderId)
            {
                model.Labels = game.GameLabels.ToList();
            }
            else if (gamePagePartId == pd.GamePageParts.part_screens.OrderId)
            {
                model.Screenshots = game.Screenshots.ToList();
                model.IsSingleGamePage = true;
            }
            return model;
        }
        #endregion
    }
}
