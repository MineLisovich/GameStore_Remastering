using GameStore.BLL.Infrastrcture;
using GameStore.BLL.Infrastrcture.Identity;
using GameStore.BLL.Services.GamesServices;
using GameStore.WEB.Areas.Admin.Models.GamesModels;
using GameStore.WEB.Infrastrcture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace GameStore.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = IdentityUserPolicy.role_adminOnly)]
    public class GamesManagerController : Controller
    {
        private readonly IGameService _gameService;
        public GamesManagerController(IGameService gameService)
        {
            _gameService = gameService;
        }
        #region PUBLIC METHODS - GET
        [HttpGet]
        public IActionResult GetGamesList()
        {
            ShowGamesModel model = new();
            model.LastAction = GetInfoAboutLastActionFromTempData(TempData);
            return View(model);
        }

        [HttpGet]
        public async Task <IActionResult> LoadGamesData (string nameGame = "")
        {
            ShowGamesModel model = new();
            model.Games = await _gameService.GetGamesAsync(nameGame);
            return PartialView("_Partial.GamesList.Show",model);
        }

        [HttpGet]
        public async Task <IActionResult> GameCreateEditForm(long gameId = 0)
        {
            DataGameModel model = await CreateDataGameModel(gameId);
            return View(model);
        }

        #endregion

        #region PUBLIC METHODS - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> CreateGame (DataGameModel model)
        {
            ResultServiceModel result = await _gameService.CreateGameAsync(model.Game);
            StandartUserActionTypes actionTypes = new();
            TempData = SetTempDataForInfoAboutLastAction(result, actionTypes.Create.Id);
            return RedirectToAction(nameof(GetGamesList));
        }
        #endregion

        #region PRIVATE METHODS
        private async Task<DataGameModel> CreateDataGameModel(long gameId)
        {
            DataGameModel model = new();

            //CREATE MODE
            if (gameId == 0) 
            { 
                model.Game = new();

            }
            //EDIT MODE
            else
            {
                model.Game = await _gameService.GetGameByIdAsync(gameId);
                model.Game.GameGanresIds = (model.Game.GameGanres.Count() > 0) ? model.Game.GameGanres.Select(x=>x.Id).ToArray() : [];
                model.Game.GamePlatformsIds = (model.Game.GamePlatforms.Count() > 0) ? model.Game.GamePlatforms.Select(x=>x.Id).ToArray() : [];
                model.Game.GameLabelsIds = (model.Game.GameLabels.Count() > 0) ? model.Game.GameLabels.Select(x=>x.Id).ToArray() : [];
            }

            //SelectList
            await CreateSelectList(model);

            return model;
        }

        private async Task<DataGameModel> CreateSelectList(DataGameModel model)
        {
            model.SelectListItemsGameGenres = new SelectList(await _gameService.GetGenresForSelectList(), "Id","Name");
            model.SelectListItemsGameDevelopers = new SelectList(await _gameService.GetDevelopersForSelectList(), "Id", "Name");
            model.SelectListItemsGamePlatforms = new SelectList(await _gameService.GetPlatromsForSelectList(), "Id", "Name");
            model.SelectListItemsGameLabels = new SelectList(await _gameService.GetLabelsForSelectList(), "Id", "Name");

            return model;
        }
        #endregion

        #region  PRIVATE METHODS - TEMPDATA
        private UserActionResult GetInfoAboutLastActionFromTempData(ITempDataDictionary TempData)
        {
            UserActionResult lastAction = (TempData.ContainsKey("LastAction")) ? JsonConvert.DeserializeObject<UserActionResult>((string)TempData["LastAction"]) : new();
            return lastAction;
        }
        private ITempDataDictionary SetTempDataForInfoAboutLastAction(ResultServiceModel result, int actionTypeId)
        {
            StandartUserActionTypes actionTypes = new();
            string mainMessage = "";

            if(actionTypeId == actionTypes.Create.Id)
            {
                mainMessage = (result.IsSucceeded) ? "Игра успешно создана" : "Не удалось создать игру";
            }
            else if(actionTypeId == actionTypes.Edit.Id)
            {
                mainMessage = (result.IsSucceeded) ? "Данные об игре изменены" : "Не удалось изменить данные об игре";
            }
            else if(actionTypeId == actionTypes.Delete.Id)
            {
                mainMessage = (result.IsSucceeded) ? "Игра успешно удалена" : "Не уадалось удалить игру";
            }

            UserActionResult lastAction = new();
            lastAction.Id = actionTypeId;
            lastAction.IsSuccess = result.IsSucceeded;
            lastAction.DopInfo = mainMessage + ". " + result.ErrorMes;
            TempData["LastAction"] = JsonConvert.SerializeObject(lastAction);
            return TempData;

        }

        #endregion
    }
}
