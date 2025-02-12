using GameStore.BLL.Infrastrcture.Models;
using GameStore.BLL.Services.CatalogServices;
using GameStore.WEB.Infrastrcture;
using GameStore.WEB.Models.HomeModels.CatalogModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;

namespace GameStore.WEB.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;
        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        #region PUBLIC METHODS - GET
        [HttpGet]
        [AllowAnonymous]
        public async Task <IActionResult> Index()
        {
            CatalogModel model = await CreateShowCatalogModel(TempData);
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task <IActionResult> LoadGamesData(string nameGame = "", int pageNumber = 1, int pageSize = 50)
        {
            CatalogModel model = new();
            model.Games = await _catalogService.GetGamesAsync(nameGame, pageNumber, pageSize);
            model.HasMoreGames = model.Games.Count == pageSize; // Проверяем, есть ли еще игры
            return PartialView("_Partial.Catalog.Games", model);
        }
        #endregion

        #region PUBLIC METHODS - POST
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> FilterGamesData([FromBody] FilterGamesRequest request)
        {
            CatalogModel model = new();
            model.Games = await _catalogService.GetFromFilteredGamesAsync(request);
            return PartialView("_Partial.Catalog.Games", model);
        }
        #endregion

        #region PRIVATE METHODS - TEMPDATA
        private UserActionResult GetInfoAboutLastActionFromTempData(ITempDataDictionary TempData)
        {
            UserActionResult lastAction = (TempData.ContainsKey("LastAction")) ? JsonConvert.DeserializeObject<UserActionResult>((string)TempData["LastAction"]) : new();
            return lastAction;
        }
        #endregion

        #region PRIVATE METHODS
        private async Task<CatalogModel> CreateShowCatalogModel(ITempDataDictionary TempData)
        {
            CatalogModel model = new();
            model.LastAction = GetInfoAboutLastActionFromTempData(TempData);
            model.SelectItemsGenres = new SelectList(await _catalogService.GetGenresForSelectListAsync(), "Id", "Name");
            model.SelectItemsDevelopers = new SelectList(await _catalogService.GetDevelopersForSelectListAsync(), "Id", "Name");
            model.SelectItemsLables = new SelectList(await _catalogService.GetLabelsForSelectListAsync(), "Id", "Name");
            model.SelectItemsPlatforms = new SelectList(await _catalogService.GetPlatformsForSelectListAsync(), "Id", "Name");

            return model;
        }
        #endregion


    }
}
