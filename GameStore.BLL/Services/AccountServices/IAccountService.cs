using GameStore.DAL.Entities.Identity;

namespace GameStore.BLL.Services.AccountServices
{
    public interface IAccountService
    {
        /// <summary>
        /// Регистрация пользователя в системе
        /// </summary>
        /// <param name="email">Эл. почта пользователя</param>
        /// <param name="password">Пароль</param>
        /// <returns>true or false</returns>
        Task<AppUser> CreateAccountAsync(string customUserName,string email, string password);
    }
}
