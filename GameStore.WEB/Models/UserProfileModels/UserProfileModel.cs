using GameStore.BLL.DTO.Identity;
using GameStore.WEB.Infrastrcture;
using GameStore.WEB.Models.AccountModels;
using System.ComponentModel.DataAnnotations;

namespace GameStore.WEB.Models.UserProfileModels
{
    /// <summary>
    /// view model - Профиль пользователя
    /// </summary>
    public class UserProfileModel
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        public AppUserDTO AppUser { get; set; }

        public List<ShoppingCartDTO> ShoppingCarts { get; set; } = new();

        /// <summary>
        /// Смена пароля
        /// </summary>
        public AccountPasswordModel ChangePassword { get; set; }

        /// <summary>
        /// Результат действий пользователя
        /// </summary>
        public UserActionResult LastAction { get; set; }

        /// <summary>
        /// Вывод ошибки
        /// </summary>
        public NotFoundResultModel NotFound { get; set; }

        //
        public bool IsChangePassword {  get; set; }

        public IFormFile uploadAvarar { get; set; }
    }
}
