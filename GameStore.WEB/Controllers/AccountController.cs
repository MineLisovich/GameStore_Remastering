﻿using GameStore.BLL.Services;
using GameStore.BLL.Services.AccountServices;
using GameStore.DAL.Entities.Identity;
using GameStore.WEB.Models.AccountModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WEB.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAccountService _accountService;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAccountService accountService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
        }

        #region PUBLIC METHODS - GET  
        //Страница авторизации
        [HttpGet]
        public IActionResult Login() => View();

        //Страница регистрации
        [HttpGet]
        public IActionResult CreateAccount() => View();

        //Страница - аккаунт заблокирован
        [HttpGet]
        public IActionResult AccountIsBlocked() => View();

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region PUBLIC METHODS - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountModel model)
        {
            AppUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
            {
                ModelState.AddModelError(nameof(AccountModel.Email), "Неверный Логин или Пароль");
                ModelState.AddModelError(nameof(AccountModel.Password), "Неверный Логин или Пароль");
                return View(model);
            }
            //Разлогин. тек. пользователя
            await _signInManager.SignOutAsync();

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);

            if (result.Succeeded)
            {
                //Если пользователь входит после временной блокировки (если 5 раз не правильно ввести пароль), то очищаем поля отв. за блокировку
                if (user.LockoutEnd is not null)
                {
                    await _userManager.SetLockoutEndDateAsync(user, null);
                }

                //Запишем дату авторизации в поле LastVisit
                user.LastVisit = DateTime.UtcNow;
                IdentityResult updateLastVisit = await _userManager.UpdateAsync(user);

                //Если изменение даты прошло успешно
                if (updateLastVisit.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else if(result.RequiresTwoFactor)
            {

            }
            else if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(AccountIsBlocked));
            }
            int maxFailedAccessAttempts = 5;
            maxFailedAccessAttempts = maxFailedAccessAttempts - user.AccessFailedCount;
            ModelState.AddModelError(nameof(model.Password), $"Неверный Логин или Пароль.\nПопыток осталось: {maxFailedAccessAttempts}");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAccount(AccountModel model)
        {
          
            AppUser createUser = await _accountService.CreateAccountAsync(model.Email, model.Password);
            if (createUser is not null)
            {
                await _signInManager.SignInAsync(createUser, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(nameof(AccountModel.Email), "При создании аккаунта произошла ошибка");
                ModelState.AddModelError(nameof(AccountModel.Password), "При создании аккаунта произошла ошибка");
                return View(model);
            }

        }
        #endregion

        #region PRIVATE METHODS
        #endregion
    }
}
