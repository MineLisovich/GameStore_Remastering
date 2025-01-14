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
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService) 
        {
            _homeService = homeService;
        }

        #region PUBLIC METHODS - GET
        [HttpGet]
        public async Task <IActionResult> Index()
        {
            HomePageModel model = await CreateHomePageModel(TempData);
            return View(model);
        }
        [HttpGet]
        public IActionResult Catalog()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Discounts()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Support()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GamePage(long gameId)
        {
            return View();
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
        #endregion
    }
}
