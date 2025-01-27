using GameStore.BLL.Services.CatalogServices;
using GameStore.WEB.Models.HomeModels.CatalogModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Catalog()
        {
            CatalogModel model = new();
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task <IActionResult> LoadGamesData(string nameGame = "")
        {
            CatalogModel model = new();
            model.Games = await _catalogService.GetGamesAsync(nameGame);
            return PartialView("_Partial.Catalog.Games", model);
        }
        #endregion

        #region PUBLIC METHODS - POST
        #endregion

        #region PRIVATE METHODS - TEMPDATA
        #endregion

        #region PRIVATE METHODS
        #endregion


    }
}
