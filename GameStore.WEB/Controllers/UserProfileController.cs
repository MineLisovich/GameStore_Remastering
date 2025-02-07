using GameStore.BLL.DTO.Identity;
using GameStore.BLL.Infrastrcture;
using GameStore.BLL.Predefined;
using GameStore.BLL.Services.EmailService;
using GameStore.BLL.Services.UserProfileServices;
using GameStore.DAL.Entities.Identity;
using GameStore.WEB.Infrastrcture;
using GameStore.WEB.Models.UserProfileModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace GameStore.WEB.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly IUserProfileService _userProfileService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        public UserProfileController(IUserProfileService userProfileService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IEmailService emailService)
        {
            _userProfileService = userProfileService;
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
        }

        #region PUBLIC METHODS - GET
        [HttpGet]
        public async Task<IActionResult> GetUserProfile()
        {
            UserProfileModel model = await CreateShowUserProfileModel(TempData);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetPartialWorkOnData(string userId, bool isChangePassword)
        {
            UserProfileModel model = await CreateDataUserProfileModel(userId, isChangePassword);
            return PartialView("_Partial.EditUserData", model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmailStepOne()
        {
            ResultServiceModel result = new();
            StandartUserActionTypes actionTypes = new();
            AppUserDTO currentUser = await _userProfileService.GetUserDataByEmailAsync(User.Identity.Name);
            if (currentUser is null)
            {
                result.IsSucceeded = false;
                result.ErrorMes = "При отправке сообщения на указанную почту произошла ошибка";
                TempData = SetTempDataForInfoAboutLastAction(result, actionTypes.СonfirmEmail.Id);
                return RedirectToAction(nameof(GetUserProfile));
            }

            string code = await _userManager.GenerateEmailConfirmationTokenAsync(currentUser);
            string callbackUrl = Url.Action("ConfirmEmailStepTwo", "UserProfile", new { userId = currentUser.Id, code = code }, protocol: HttpContext.Request.Scheme);

            try
            {
                string plainTextBody = $"Please confirm your email by PET PROJECT";
                string htmlBody = $"Для потдтверждение почты просто перейдите по этой ссылке: <a href='{callbackUrl}'>link</a>";
                await _emailService.SendEmailAsync(currentUser.Email, plainTextBody, htmlBody);
            }
            catch
            {
                result.IsSucceeded = false;
                result.ErrorMes = "При отправке сообщения на указанную почту произошла ошибка";
                TempData = SetTempDataForInfoAboutLastAction(result, actionTypes.СonfirmEmail.Id);
                return RedirectToAction(nameof(GetUserProfile));
            }
            result.IsSucceeded = true;
            TempData = SetTempDataForInfoAboutLastAction(result, actionTypes.СonfirmEmail.Id);
            return RedirectToAction(nameof(GetUserProfile));
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmailStepTwo(string userId, string code)
        {
            ResultServiceModel result = await _userProfileService.ConfirmEmailAsync(userId, code);
            return RedirectToAction(nameof(GetUserProfile));
        }

        [HttpGet]
        public async Task<IActionResult> UnlinkEmail()
        {
            StandartUserActionTypes actionTypes = new();
            ResultServiceModel result = await _userProfileService.UnlinkEmailAsync(User.Identity.Name);
            TempData = SetTempDataForInfoAboutLastAction(result, actionTypes.UnlinkEmail.Id);
            return RedirectToAction(nameof(GetUserProfile));
        }

        [HttpGet]
        public async Task<IActionResult> TwoFactorManager(bool isEndable)
        {
            StandartUserActionTypes actionTypes = new();
            ResultServiceModel result = await _userProfileService.EnableOrDisable2FAAsync(User.Identity.Name, isEndable);
            TempData = SetTempDataForInfoAboutLastAction(result, (isEndable is true) ? actionTypes.Enable2FA.Id : actionTypes.Disable2FA.Id);
            return RedirectToAction(nameof(GetUserProfile));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUserAccount(string userId)
        {
            ResultServiceModel result = await _userProfileService.DeleteUserAccountAsync(userId);

            if (result.IsSucceeded is false)
            {
                StandartUserActionTypes actionTypes = new();
                TempData = SetTempDataForInfoAboutLastAction(result, actionTypes.Delete.Id);
                return RedirectToAction(nameof(GetUserProfile));
            }
            else
            {

                await DeleteCookie(false);
                return Json(result);
            }
        }

        [HttpGet]
        public IActionResult AccountRefill()
        {
            RefillModel model = new();
            return View(model);
        }
        #endregion

        #region PUBLIC METHODS - POST
        [HttpPost]
        public async Task<IActionResult> EditUserProfile(UserProfileModel model)
        {
            StandartUserActionTypes actionTypes = new();
            ResultServiceModel result = await _userProfileService.EditUserProfileDataAsync(model.AppUser, model.uploadAvarar);
            //Создаём темпдату о результатах действия пользователя
            TempData = SetTempDataForInfoAboutLastAction(result, actionTypes.Edit.Id);
            if (result.IsSucceeded is true && model.AppUser.IsChangeEmail is false)
            {
                return RedirectToAction(nameof(GetUserProfile));
            }
            else if (result.IsSucceeded is true && model.AppUser.IsChangeEmail is true)
            {
                await DeleteCookie(true);
            }
            return RedirectToAction(nameof(GetUserProfile));
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserPassword(UserProfileModel model)
        {
            StandartUserActionTypes actionTypes = new();
            ResultServiceModel result = await _userProfileService.ChangeUserPasswordAsync(User.Identity.Name, model.ChangePassword.Password);
            TempData = SetTempDataForInfoAboutLastAction(result, actionTypes.PasswordChange.Id);
            return RedirectToAction(nameof(GetUserProfile));
        }

        [HttpPost]
        public async Task <IActionResult> AccountRefill(RefillModel model)
        {
            ResultServiceModel result = await _userProfileService.AccountRefill(model.PaymentSum.Value, User.Identity.Name);
            StandartUserActionTypes actionTypes = new();
            TempData = SetTempDataForInfoAboutLastAction(result, actionTypes.ReplenishBalance.Id);
            return RedirectToAction(nameof(GetUserProfile));
        }
        #endregion

        #region PRIVATE METHODS
        private async Task<UserProfileModel> CreateShowUserProfileModel(ITempDataDictionary TempData)
        {
            UserProfileModel model = new();
            model.AppUser = await _userProfileService.GetUserDataByEmailAsync(User.Identity.Name);
            model.LastAction = GetInfoAboutLastActionFromTempData(TempData);
            return model;
        }

        private async Task<UserProfileModel> CreateDataUserProfileModel(string userId, bool isChangePassword)
        {
            UserProfileModel model = new();
            model.AppUser = await _userProfileService.GetUserDataByEmailAsync(userId);
            model.IsChangePassword = isChangePassword;
            if (model.IsChangePassword is true)
            {
                model.ChangePassword = new();
            }

            return model;

        }


        private async Task<IActionResult> DeleteCookie(bool isRedirect)
        {
            //Разлогин. тек. пользователя
            await _signInManager.SignOutAsync();
            //Удаляем куки связанные с 2FA (Identity.TwoFactorRememberMe - название по умалчанию)
            HttpContext.Response.Cookies.Delete("Identity.TwoFactorRememberMe", new CookieOptions { Expires = DateTime.Now.AddDays(-10) });
            //Удаляем куки 
            HttpContext.Response.Cookies.Delete("GSCookie", new CookieOptions { Expires = DateTime.Now.AddDays(-10) });

            if (isRedirect is true)
            {
                return RedirectToAction("Index", "Home");
            }
            return Json(null);
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
            if (actionTypeId == actionTypes.Edit.Id)
            {
                mainMessage = (result.IsSucceeded) ? "Данные профиля успешно изменены" : "Не удалось изменить данные профиля";
            }
            else if (actionTypeId == actionTypes.PasswordChange.Id)
            {
                mainMessage = (result.IsSucceeded) ? "Пароль успешно изменён" : "Не удалось изменить пароль";
            }
            else if (actionTypeId == actionTypes.СonfirmEmail.Id)
            {
                mainMessage = (result.IsSucceeded) ? "На Ваш Email отправленна ссылка для подтвердения" : "Не удалось подтвердить Email";
            }
            else if (actionTypeId == actionTypes.UnlinkEmail.Id)
            {
                mainMessage = (result.IsSucceeded) ? "Email успешно отвязан" : "Не удалось отвязать Email";
            }
            else if (actionTypeId == actionTypes.Enable2FA.Id)
            {
                mainMessage = (result.IsSucceeded) ? "2FA Включена" : "Не удалось включить 2FA";
            }
            else if (actionTypeId == actionTypes.Disable2FA.Id)
            {
                mainMessage = (result.IsSucceeded) ? "2FA Выключена" : "Не удалось выключить 2FA";
            }
            else if (actionTypeId == actionTypes.Delete.Id)
            {
                mainMessage = (result.IsSucceeded) ? "Удаление прошло успешно" : "Не удалось удалить";
            }
            else if(actionTypeId == actionTypes.ReplenishBalance.Id)
            {
                mainMessage = (result.IsSucceeded) ? "Баланс успешно пополнен" : "Не удалось пополнить баланс";
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
