using AutoMapper;
using GameStore.BLL.DTO.Identity;
using GameStore.BLL.Infrastrcture;
using GameStore.BLL.Predefined;
using GameStore.BLL.Services.EmailService;
using GameStore.DAL.Domain;
using GameStore.DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameStore.BLL.Services.UserManagerServices
{
    public class UserManagerService : IUserManagerService
    {
        private readonly GsDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;

        public UserManagerService(GsDbContext context, IMapper mapper, 
                                UserManager<AppUser> userManager, IEmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        /// <summary>
        /// Получить весь список пользователей
        /// </summary>
        /// <returns>Список пользователей</returns>
        public async Task<List<AppUserDTO>> GetAllAppUserAsync()
        {
            List<AppUser> users = await _context.AppUsers.ToListAsync();
            return _mapper.Map<List<AppUserDTO>>(users);
        }

        /// <summary>
        /// Получить пользователя по Email
        /// </summary>
        /// <param name="userId">Email пользователя</param>
        /// <returns>Пользователь</returns>
        public async Task<AppUserDTO> GetAppUserByEmailAsync(string userId)
        {
            AppUser user = await _context.AppUsers.Where(x=>x.Email == userId).FirstOrDefaultAsync();
            return _mapper.Map<AppUserDTO>(user);
        }

        /// <summary>
        /// Создать нового пользователя
        /// </summary>
        /// <param name="appUserDTO">Заполненная модель</param>
        /// <returns>Результат действия сервиса (true || false)</returns>
        public async Task<ResultServiceModel> GreateUserAsync(AppUserDTO appUserDTO)
        {
            ResultServiceModel result = new();
            PredefinedManager pd = new();

            long currentMaxCustomId = await _context.AppUsers.MaxAsync(x => x.CustomUserId);

            appUserDTO.CustomUserId = currentMaxCustomId+1;
            appUserDTO.TwoFactorEnabled = false;
            appUserDTO.EmailConfirmed = false;
            appUserDTO.LockoutEnabled = true;
            appUserDTO.LockoutEnd = null;
            appUserDTO.UserName = appUserDTO.Email;
            appUserDTO.CustomUserName = appUserDTO.Email;

            AppUser user = _mapper.Map<AppUser>(appUserDTO);
           
            if (user.Email is null || user.UserRoleName is null)
            { result.IsSucceeded = false; result.ErrorMes = "Не введён Email"; return result; }

            //Создаём временный пароль который отправим пользователю на почту
            string tempPassword = "GS" + Guid.NewGuid().ToString();

            IdentityResult resultCreate = await _userManager.CreateAsync(user, tempPassword);
            IdentityResult resultRoleMain = await _userManager.AddToRoleAsync(user, user.UserRoleName);

            if (resultCreate.Succeeded && resultRoleMain.Succeeded)
            {
                await _emailService.SendEmailAsync(user.Email,"Аккаунт был создан", $"Ваш временный пароль для входа. {tempPassword} (Рекомендация. Смените пароль в Вашем профиле.)");
                result.IsSucceeded = true;
                return result;
            }

            result.IsSucceeded = false;
            result.ErrorMes = DefaultErrorMessages.dontSave;
            return result;
        }


        /// <summary>
        /// Редактирование существующего пользователя
        /// </summary>
        /// <param name="currentUserEmail">Email текущего пользователя</param>
        /// <param name="appUserDTO">Заполненная модель</param>
        /// <returns>Результат действия сервиса (true || false)</returns>
        public async Task<ResultServiceModel> UpdateUserAsync(string currentUserEmail, AppUserDTO appUserDTO)
        {
            ResultServiceModel result = new();
            AppUser user = await _context.AppUsers.Where(x=>x.Id == appUserDTO.Id).FirstOrDefaultAsync();
            if (user is null) 
            { result.IsSucceeded = false; result.ErrorMes = DefaultErrorMessages.recordNoExist; return result; }

            if (user.Email == currentUserEmail)
            { result.IsSucceeded = false; result.ErrorMes = "Нельзя редактировать своиже данные. Но если сменился Email, то вы можете поменять его через свой профиль"; return result; }


            if(user.Email != appUserDTO.Email)
            {
                user.Email = appUserDTO.Email;
                user.UserName = appUserDTO.Email;
                user.TwoFactorEnabled = false;
                user.EmailConfirmed = false;
            }

            //Если поменялась роль
            if(user.UserRoleName != appUserDTO.UserRoleName)
            {
                user.UserRoleName = appUserDTO.UserRoleName;

                //Так как у пользователя может быть токлько 1 роль, нужно удалить предыдущую роль
                List<string> userRoles = (List<string>)await _userManager.GetRolesAsync(user);
                foreach (string userRole in userRoles.ToList())
                {
                    IdentityResult delroleresult = await _userManager.RemoveFromRoleAsync(user, userRole);
                    if (delroleresult.Succeeded) { }
                }

                //Добавим выбранную роль и добавим вспомогательную роль
                try { await _userManager.AddToRoleAsync(user, appUserDTO.UserRoleName);  }
                catch { result.IsSucceeded = false; result.ErrorMes = DefaultErrorMessages.dontSave; return result; }
            }

            try { await _userManager.UpdateAsync(user); }
            catch { result.IsSucceeded = false; result.ErrorMes = DefaultErrorMessages.dontSave; return result; }

            //Если поменялся статус
            if (appUserDTO.IsBanned is true)
            {
                if(user.LockoutEnd is null)
                {
                    try { await _userManager.SetLockoutEndDateAsync(user, System.DateTimeOffset.MaxValue); }
                    catch { result.IsSucceeded = false; result.ErrorMes = DefaultErrorMessages.dontSave; return result; }
                }
            }
            else
            {
                if (user.LockoutEnd is not null)
                {
                    try { await _userManager.SetLockoutEndDateAsync(user, null); }
                    catch { result.IsSucceeded = false; result.ErrorMes = DefaultErrorMessages.dontSave; return result; }
                }
            }


            //Если всё прошло успешно
            result.IsSucceeded = true;
            return result;
        }

        /// <summary>
        /// Удаление аккаунта пользователя
        /// </summary>
        /// <param name="currentUserEmail">Email текущего пользователя</param>
        /// <param name="userId">Email пользователя</param>
        /// <returns>Результат действия сервиса (true || false)</returns>
        public async Task<ResultServiceModel> DeleteUserAsync(string currentUserEmail, string userId)
        {
            ResultServiceModel result = new();

            AppUser user = await _context.AppUsers.Where(x=>x.Email == userId).FirstOrDefaultAsync();
            if (user is null) 
            { result.IsSucceeded = false; result.ErrorMes = DefaultErrorMessages.recordNoExist; return result; }

            if(user.Email == currentUserEmail)
            { result.IsSucceeded = false; result.ErrorMes = "Нельзя удалить самого себя."; return result; }

            try
            {
                _context.AppUsers.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch { result.IsSucceeded = false; result.ErrorMes = DefaultErrorMessages.dontSave; return result; }

            result.IsSucceeded = true;
            return result;
        }
    }
}
