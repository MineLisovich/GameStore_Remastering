using GameStore.BLL.DTO.Dictionaries;
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
            SingleGamePageModel model = await CreateSingleGamePageModel(gameId,TempData);
            return View(model);
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

            if (model.Game is not null)
            {
                model.Screenshots = model.Game.Screenshots;
                model.Genres = model.Game.GameGanres;
                model.Platforms = model.Game.GameKeys.DistinctBy(x=>x.PlatformId).Select(x=>x.Platform).ToList();
            }
            else
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
        #endregion
    }
}
