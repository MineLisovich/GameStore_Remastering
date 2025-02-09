using AutoMapper;
using GameStore.BLL.DTO.Games;
using GameStore.BLL.DTO.Identity;
using GameStore.BLL.Infrastrcture;
using GameStore.BLL.Infrastrcture.Singletons;
using GameStore.DAL.Domain;
using GameStore.DAL.Entities.Games;
using GameStore.DAL.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GameStore.BLL.Services.UserProfileServices
{
    public class UserProfileService : IUserProfileService
    {
        private readonly GsDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public UserProfileService(GsDbContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<AppUserDTO> GetUserDataByEmailAsync(string email)
        {
            AppUser user = await _context.AppUsers.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (user is null) { return null; }

            AppUserDTO userDTO = _mapper.Map<AppUserDTO>(user);
            return userDTO;
        }

        public async Task<ResultServiceModel> EditUserProfileDataAsync(AppUserDTO userDTO, IFormFile uploadAvarar)
        {
            ResultServiceModel result = new();
            AppUser user = await _context.AppUsers.Where(x => x.Id == userDTO.Id).FirstOrDefaultAsync();
            if (uploadAvarar is not null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var stream = uploadAvarar.OpenReadStream())
                using (var binaryReader = new BinaryReader(stream))
                {
                    imageData = binaryReader.ReadBytes((int)uploadAvarar.Length);
                }
                user.AvatarImage = imageData;
                user.AvatarName = uploadAvarar.FileName;

            }

            if (user.CustomUserName != userDTO.CustomUserName)
            {
                user.CustomUserName = userDTO.CustomUserName;
            }

            if (user.Email != userDTO.Email && userDTO.IsChangeEmail is true)
            {
                user.Email = userDTO.Email;
                user.NormalizedEmail = userDTO.Email.ToUpper();
                user.UserName = userDTO.Email;
                user.NormalizedUserName = userDTO.Email.ToUpper();
                user.EmailConfirmed = false;
                user.TwoFactorEnabled = false;
            }

            try
            {
                _context.AppUsers.Update(user);
                await _context.SaveChangesAsync();
            }
            catch { result.IsSucceeded = false; result.ErrorMes = "Ошибка"; return result; }
            result.IsSucceeded = true;
            return result;
        }

        public async Task<ResultServiceModel> ConfirmEmailAsync(string userId, string code)
        {
            ResultServiceModel result = new();
            if (userId is null || code is null)
            { result.IsSucceeded = false; result.ErrorMes = "При продтверждении Email произошла ошибка"; return result; }

            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            { result.IsSucceeded = false; result.ErrorMes = "При продтверждении Email произошла ошибка"; return result; }

            IdentityResult result_confirmEmail = await _userManager.ConfirmEmailAsync(user, code);
            if (result_confirmEmail.Succeeded is false)
            { result.IsSucceeded = false; result.ErrorMes = "При продтверждении Email произошла ошибка"; return result; }

            result.IsSucceeded = true;
            return result;
        }

        public async Task<ResultServiceModel> UnlinkEmailAsync(string email)
        {
            ResultServiceModel result = new();

            AppUser user = await _context.AppUsers.Where(x => x.Email == email).FirstOrDefaultAsync();

            if (user is null) { result.IsSucceeded = false; result.ErrorMes = "При отвязке Email произошла ошибка"; return result; }

            user.EmailConfirmed = false;
            try
            {
                _context.AppUsers.Update(user);
                await _context.SaveChangesAsync();
            }

            catch { result.IsSucceeded = false; result.ErrorMes = "При отвязке Email произошла ошибка"; return result; }


            result.IsSucceeded = true;
            return result;
        }

        public async Task<ResultServiceModel> EnableOrDisable2FAAsync(string email, bool isEnable)
        {

            ResultServiceModel result = new();
            AppUser user = await _context.AppUsers.Where(x => x.Email == email).FirstOrDefaultAsync();

            string errorWord = (isEnable is true) ? "включении" : "выключении";

            if (user is null) { result.IsSucceeded = false; result.ErrorMes = "При " + errorWord + " произошла ошибка"; return result; }


            if (user.EmailConfirmed is true)
            {
                user.TwoFactorEnabled = isEnable;
            }
            else { result.IsSucceeded = false; result.ErrorMes = "Вы не можете включить 2FA, так вы не подтвердили свою почту"; return result; }


            try
            {
                _context.AppUsers.Update(user);
                await _context.SaveChangesAsync();
            }

            catch { result.IsSucceeded = false; result.ErrorMes = "При " + errorWord + " произошла ошибка"; return result; }

            result.IsSucceeded = true;
            return result;
        }

        public async Task<ResultServiceModel> ChangeUserPasswordAsync(string email, string password)
        {
            ResultServiceModel result = new();

            AppUser user = await _context.AppUsers.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (user is null) { result.IsSucceeded = false; result.ErrorMes = "Пользователь не найден"; return result; }

            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            IdentityResult result_changePassword = await _userManager.ResetPasswordAsync(user, resetToken, password);
            if (!result_changePassword.Succeeded)
            { result.IsSucceeded = false; result.ErrorMes = "Ошибка. Попробуйте позже"; return result; }

            result.IsSucceeded = true;
            return result;
        }

        public async Task<ResultServiceModel> DeleteUserAccountAsync(string email)
        {
            ResultServiceModel result = new();
            AppUser user = await _context.AppUsers.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (user is null) { result.IsSucceeded = false; result.ErrorMes = "Пользователь не найден"; return result; }

            _context.AppUsers.Remove(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch { result.IsSucceeded = false; result.ErrorMes = "Ошибка. Попробуйте позже"; return result; }

            result.IsSucceeded = true;
            return result;
        }

        public async Task<ResultServiceModel> AccountRefill(decimal payment, string userEmail)
        {
            ResultServiceModel result = new();

            AppUser user = await _context.AppUsers.Where(x => x.Email == userEmail).FirstOrDefaultAsync();
            if (user is not null)
            {
                user.Balance += payment;
                try
                {
                    _context.AppUsers.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch { result.IsSucceeded = false; result.ErrorMes = ""; return result; }
            }

            result.IsSucceeded = true;
            return result;
        }

        public async Task<List<ShoppingCartDTO>> GetLastFiveOrdersUser(string userEmail)
        {

            AppUser user = await _context.AppUsers.Where(x => x.Email == userEmail).FirstOrDefaultAsync();
            if (user is null) { return new(); }
            IQueryable<ShoppingCart> sql = GetSqlQuery(user.Id);
            List<ShoppingCart> shoppingCarts = await sql.Take(5).ToListAsync();
            return _mapper.Map<List<ShoppingCartDTO>>(shoppingCarts);
        }

        public async Task<List<ShoppingCartDTO>> GetOrdersUser(string userEmail)
        {
            AppUser user = await _context.AppUsers.Where(x => x.Email == userEmail).FirstOrDefaultAsync();
            if (user is null) { return new(); }
            IQueryable<ShoppingCart> sql = GetSqlQuery(user.Id);
            List<ShoppingCart> shoppingCarts = await sql.ToListAsync();
            return _mapper.Map<List<ShoppingCartDTO>>(shoppingCarts);
        }

        private IQueryable<ShoppingCart> GetSqlQuery(string userId)
        {
            IQueryable<ShoppingCart> sql = _context.ShoppingCarts.OrderByDescending(x => x.PaymentDate)
                                                                           .Where(x => x.IsActive == false && x.UserId == userId)
                                                                           .Include(gk => gk.GamesKeys).ThenInclude(game => game.Game)
                                                                           .Include(gk => gk.GamesKeys).ThenInclude(pl => pl.Platform);
            return sql;
        }

        public async Task<string> GetKeyAsync(long gameKeyId)
        {
            string key = await GetDecryptGameKey(gameKeyId);
            return key;
        }

        private async Task<string> GetDecryptGameKey(long gameKeyId)
        {
            GameKey gameKey = await _context.GameKeys.Where(x => x.Id == gameKeyId).FirstOrDefaultAsync();
            string key = "Ошибка. Попробуйте позже или дргуой способ получения ключа.";

            if (gameKey is not null)
            {
                GameKeyCryptography gkc = GameKeyCryptography.GetIstance();
                key = gkc.Decrypt(gameKey.Key);
            }

            return key;
        }

        public async Task<GameDTO> GetGameByGameKeyIdAsync(long gameKeyId)
        {
            GameKey gameKey = await _context.GameKeys.Where(x => x.Id == gameKeyId).Include(game => game.Game).FirstOrDefaultAsync();
            return _mapper.Map<GameDTO>(gameKey.Game);
        }
    }
}
