using GameStore.BLL.DTO.Dictionaries;
using GameStore.BLL.Infrastrcture;
using GameStore.BLL.Infrastrcture.Identity;
using GameStore.BLL.Services.DictionariesServices;
using GameStore.DAL.Entities.Dictionaries;
using GameStore.WEB.Areas.Admin.Models.DictionariesModels;
using GameStore.WEB.Infrastrcture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace GameStore.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = IdentityUserPolicy.role_adminOnly)]
    public class DictionariesController : Controller
    {
        private readonly DictionaryService<Genre, GenreDTO> _genreService;
        private readonly DictionaryService<GameDeveloper, GameDeveloperDTO> _gameDevService;
        private readonly DictionaryService<GamePlatform, GamePlatformDTO> _gamePlatformService;
        private readonly DictionaryService<GameLabel, GameLabelDTO> _gameLableService;

        public DictionariesController(DictionaryService<Genre, GenreDTO> genreService,
                                      DictionaryService<GameDeveloper, GameDeveloperDTO> gameDevService,
                                      DictionaryService<GamePlatform, GamePlatformDTO> gamePlatformService,
                                      DictionaryService<GameLabel, GameLabelDTO> gameLableService)
        {
            _genreService = genreService;
            _gameDevService = gameDevService;
            _gamePlatformService = gamePlatformService;
            _gameLableService = gameLableService;
        }


        #region PUBLIC METHODS - GET
        [HttpGet]
        public IActionResult Index()
        {
            ShowDictionatyModel model = new();
            model.LastAction = GetInfoAboutLastActionFromTempData(TempData);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetDataForTable(string sectionName)
        {
            ShowDictionatyModel model = await CreateShowModel(sectionName, TempData);
            return PartialView("_Partial.Dictionaries.List", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetPartialWorkOnData(int id, string sectionName)
        {
            DataDictionaryModel model = await CreateDataModel(id, sectionName);
            return PartialView("_Partial.Dictionaries.CreateEditForm", model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDictionary (int id, string sectionName)
        {
            ResultServiceModel result = new();
            DictionariesTypes dictionariesTypes = new();

            if (sectionName == dictionariesTypes.d_genre.SectionName)
            {
                result = await _genreService.DeleteEntity(id);
                                      
            }
            else if (sectionName == dictionariesTypes.d_gamDev.SectionName)
            {
                result = await _gameDevService.DeleteEntity(id);
                                            
            }
            else if (sectionName == dictionariesTypes.d_gamPlat.SectionName)
            {
                result = await _gamePlatformService.DeleteEntity(id);
                                          
            }
            else if (sectionName == dictionariesTypes.d_gamLabel.SectionName)
            {
                result = await _gameLableService.DeleteEntity(id);                        
            }

            StandartUserActionTypes actionTypes = new();
            int sectionOrderId = dictionariesTypes.DictionaryTypesList.Where(x=>x.SectionName == sectionName).First().OrderId;
            TempData = SetTempDataForInfoAboutLastAction(result, actionTypes.Delete.Id, sectionOrderId);
            return Json(result);
        }
        #endregion

        #region PUBLIC METHODS - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEditDictionary(DataDictionaryModel model)
        {
            ResultServiceModel result = new();
            DictionariesTypes dictionariesTypes = new();
            
            if(model.SectionOrderId == dictionariesTypes.d_genre.OrderId)
            {
                result = (model.IsCreateMode) ? await _genreService.CreateEntity(model.Genre) 
                                              : await _genreService.UpdateEntity(model.Genre);
            }
            else if (model.SectionOrderId == dictionariesTypes.d_gamDev.OrderId)
            {
                result = (model.IsCreateMode) ? await _gameDevService.CreateEntity(model.GameDeveloper)
                                              : await _gameDevService.UpdateEntity(model.GameDeveloper);
            }
            else if (model.SectionOrderId == dictionariesTypes.d_gamPlat.OrderId)
            {
                result = (model.IsCreateMode) ? await _gamePlatformService.CreateEntity(model.GamePlatform)
                                              : await _gamePlatformService.UpdateEntity(model.GamePlatform);
            }
            else if (model.SectionOrderId == dictionariesTypes.d_gamLabel.OrderId)
            {
                result = (model.IsCreateMode) ? await _gameLableService.CreateEntity(model.GameLabel)
                                              : await _gameLableService.UpdateEntity(model.GameLabel);
            }

            StandartUserActionTypes actionTypes = new();
            TempData = SetTempDataForInfoAboutLastAction(result, (model.IsCreateMode) ? actionTypes.Create.Id : actionTypes.Edit.Id ,model.SectionOrderId);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region PRIVATE METHODS 
        private async Task<ShowDictionatyModel> CreateShowModel(string sectionName, ITempDataDictionary TempData)
        {
            ShowDictionatyModel model = new();
            DictionariesTypes dictionariesTypes = new();
            if (dictionariesTypes.d_genre.SectionName == sectionName)
            {
                model.Genres = await _genreService.GetAllEntitiesAsync();
                model.DictionaryName = dictionariesTypes.d_genre.RuName;

                model.GameLabels = new();
                model.GameDevelopers = new();
                model.GamePlatforms = new();
            }
            else if (dictionariesTypes.d_gamDev.SectionName == sectionName)
            {
                model.GameDevelopers = await _gameDevService.GetAllEntitiesAsync();
                model.DictionaryName = dictionariesTypes.d_gamDev.RuName;

                model.Genres = new();
                model.GamePlatforms = new();
                model.GameLabels = new();

            }
            else if (dictionariesTypes.d_gamPlat.SectionName == sectionName)
            {
                model.GamePlatforms = await _gamePlatformService.GetAllEntitiesAsync();
                model.DictionaryName = dictionariesTypes.d_gamPlat.RuName;

                model.Genres = new();
                model.GameDevelopers = new();
                model.GameLabels = new();
            }
            else if (dictionariesTypes.d_gamLabel.SectionName == sectionName)
            {
                model.GameLabels = await _gameLableService.GetAllEntitiesAsync();
                model.DictionaryName = dictionariesTypes.d_gamLabel.RuName;

                model.Genres = new();
                model.GameDevelopers = new();
                model.GamePlatforms = new();
            }
            model.LastAction = GetInfoAboutLastActionFromTempData(TempData);

            return model;
        }

        private async Task<DataDictionaryModel> CreateDataModel(int id, string sectionName)
        {
            DataDictionaryModel model = new();
            DictionariesTypes dictionariesTypes = new();
            string titleText = string.Empty;

            //CREATE MODE
            if (id == 0)
            {
                model.IsCreateMode = true;
            }
            //EDIT MODE
            else
            {
                if (sectionName == dictionariesTypes.d_genre.SectionName)
                {
                    model.Genre = await _genreService.GetEntityById(id);
                }
                else if (sectionName == dictionariesTypes.d_gamDev.SectionName)
                {
                    model.GameDeveloper = await _gameDevService.GetEntityById(id);
                }
                else if (sectionName == dictionariesTypes.d_gamPlat.SectionName)
                {
                    model.GamePlatform = await _gamePlatformService.GetEntityById(id);
                }
                else if (sectionName == dictionariesTypes.d_gamLabel.SectionName)
                {
                    model.GameLabel = await _gameLableService.GetEntityById(id);
                }

                model.IsCreateMode = false;
            }

            model.TitleNameForm = CreateTextTitleForFormCreateEdit(sectionName);
            model.SectionOrderId = dictionariesTypes.DictionaryTypesList.Where(x=>x.SectionName == sectionName).First().OrderId;
            return model;
        }

        private string CreateTextTitleForFormCreateEdit(string sectionName)
        {
            DictionariesTypes dictionariesTypes = new();
            string titleText = string.Empty;

            if (sectionName == dictionariesTypes.d_genre.SectionName)
            {
                titleText = "Жанра";
            }
            else if (sectionName == dictionariesTypes.d_gamDev.SectionName)
            {
                titleText = "Разработчика";
            }
            else if (sectionName == dictionariesTypes.d_gamPlat.SectionName)
            {
                titleText = "Платформы";
            }
            else if (sectionName == dictionariesTypes.d_gamLabel.SectionName)
            {
                titleText = "Особенности игры";
            }

            return titleText;
        }

        private string CreateTextForLastActionTempData(int sectionOrderId)
        {
            DictionariesTypes dictionariesTypes = new();
            string txt = string.Empty;

            if (sectionOrderId == dictionariesTypes.d_genre.OrderId)
            {
                txt = "Раздел: Жанры игр";
            }
            else if (sectionOrderId == dictionariesTypes.d_gamDev.OrderId)
            {
                txt = "Раздел: Разработчики игр";
            }
            else if (sectionOrderId == dictionariesTypes.d_gamPlat.OrderId)
            {
                txt = "Раздел: Игровые платформы";
            }
            else if (sectionOrderId == dictionariesTypes.d_gamLabel.OrderId)
            {
                txt = "Раздел: Особенности игр";
            }

            return txt;
        }
        #endregion

        #region PRIVATE METHODS  - TEMPDATA
        private UserActionResult GetInfoAboutLastActionFromTempData(ITempDataDictionary TempData)
        {
            UserActionResult lastAction = (TempData.ContainsKey("LastAction")) ? JsonConvert.DeserializeObject<UserActionResult>((string)TempData["LastAction"]) : new();
            return lastAction;
        }
        private ITempDataDictionary SetTempDataForInfoAboutLastAction(ResultServiceModel result, int actionTypeId, int sectionOrderId)
        {
            StandartUserActionTypes actionTypes = new();
            string mainMessage = CreateTextForLastActionTempData(sectionOrderId);

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
