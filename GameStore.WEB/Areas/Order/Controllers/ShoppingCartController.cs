using GameStore.BLL.DTO.Identity;
using GameStore.BLL.Infrastrcture;
using GameStore.BLL.Predefined;
using GameStore.BLL.Services.ShoppingCartServices;
using GameStore.WEB.Areas.Order.Models;
using GameStore.WEB.Infrastrcture;
using GameStore.WEB.Models.HomeModels.HomePageModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;

namespace GameStore.WEB.Areas.Order.Controllers
{
    [Area("Order")]
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        #region PUBLIC METHODS - GET
        [HttpGet]
        public async Task <IActionResult> GetShoppingCart()
        {
            ShoppingCartDataModel model = await CreateShoppingCartDataModel(TempData);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteGameFromShoppingCart(bool isDeleteAll, long gameKeyId = 0)
        {
            ResultServiceModel result = await _shoppingCartService.DeleteItemsInShoppingCartAsync(User.Identity.Name, isDeleteAll, gameKeyId);
            ShoppingCartDataModel model = await CreateShoppingCartDataModel(TempData);
            StandartUserActionTypes actionTypes = new();
           
            return PartialView("_Partial.ShoppingCart.Items", model);
        }

        [HttpGet]
        public async Task<JsonResult> GetActualPriceShoppingCart()
        {
            decimal actualPrice = await _shoppingCartService.GetActualPriceShoppingCartAsync(User.Identity.Name);
            return Json(actualPrice);
        }

        [HttpGet]
        public async Task<IActionResult> GetPartialWorkOnData(long shopCrtId)
        {
            PaymentMethodsModel model =await CreatePaymentMethodsModel(shopCrtId);
            return PartialView("_Partial.ShoppingCart.PaymentMethods",model);
        }
        #endregion

        #region PUBLIC METHODS - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToShoppingCart(HomeDataModel model)
        {
            string referrerUrl = Request.Headers.Referer;

            ResultServiceModel result = await _shoppingCartService.AddToShoppingCartAsync(User.Identity.Name, model.Game.Id, model.PlatformId);

            StandartUserActionTypes actionTypes = new();
            TempData = SetTempDataForInfoAboutLastAction(result, actionTypes.AddGameToShoppingCart.Id);

            if(referrerUrl.Contains("GamePage"))
            {
                return RedirectToAction("GamePage", "Home", new { Area = "", gameId = model.Game.Id });
            }
            else if(referrerUrl.Contains("Catalog"))
            {
                return RedirectToAction("Index", "Catalog", new { Area = "" });
            }
            else if(referrerUrl.Contains("Discounts"))
            {
                return RedirectToAction("Discounts", "Home", new { Area = "" });
            }
            else
            {
                return RedirectToAction("Index","Home", new { Area = ""});
            }
           
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PaymentShoppingCart(PaymentMethodsModel model)
        {
            ResultServiceModel result = new();
            StandartUserActionTypes actionTypes = new();
            PredefinedManager pd = new();

            if(model.PaymentMethodId != pd.PaymentMethods.internal_wallet.Id)
            {
                result.IsSucceeded = false; result.ErrorMes = "Данный способ оплаты временно не доступен";
            }
            else
            {
                result = await _shoppingCartService.PaymentShoppingCartByInternalWalletAsync(User.Identity.Name, model.ShoppingCartId, model.TotalPrice);
            }
            



            TempData = SetTempDataForInfoAboutLastAction(result, actionTypes.PaymentShoppingCart.Id);
            return RedirectToAction(nameof(GetShoppingCart));
        }
        #endregion

        #region PRIVATE METHODS - TEMP DATA 
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
            if (actionTypeId == actionTypes.AddGameToShoppingCart.Id)
            {
                mainMessage = (result.IsSucceeded) ? "Игра успешно добавлена в корзину" : "Не удалось добавить игру в корзину";
            }
            else if(actionTypeId == actionTypes.Delete.Id)
            {
                mainMessage = (result.IsSucceeded) ? "Игра успешно удалена из корзины" : "Не удалось удалить игру из корзины";
            }
            else if (actionTypeId == actionTypes.CleaerAllShoppingCart.Id)
            {
                mainMessage = (result.IsSucceeded) ? "Вся корзина успешно очищена" : "Не удалось очистить всю корзину";
            }
            else if(actionTypeId == actionTypes.PaymentShoppingCart.Id)
            {
                mainMessage = (result.IsSucceeded) ? "Оплата прошла успешно. Получить ключи можете в Вашем профиле" : "Оплата не прошла. Пожалуйста попробуйте позже";
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
        private async Task<ShoppingCartDataModel> CreateShoppingCartDataModel(ITempDataDictionary TempData)
        {
            ShoppingCartDataModel model = new();
            model.ShoppingCart = await _shoppingCartService.GetActiveShoppingCartAsync(User.Identity.Name);
            model.LastAction = GetInfoAboutLastActionFromTempData(TempData);
            return model;
        }

        private async Task<PaymentMethodsModel> CreatePaymentMethodsModel(long shopCrtId)
        {
            PaymentMethodsModel model = new();
            PredefinedManager pd = new();
            ShoppingCartDTO cartDTO = await _shoppingCartService.GetSoppingCartByIdAsync(shopCrtId);
            model.ShoppingCartId = cartDTO.Id;
            model.TotalPrice = cartDTO.TotalPrice;
            model.SelectItemsPaymentMethods = new SelectList(pd.PaymentMethods.paymentMethods, "Id", "Name");
            return model;
        }
        #endregion
    }
}
