using System.ComponentModel.DataAnnotations;

namespace GameStore.WEB.Models.AccountModels
{
    /// <summary>
    /// Модель для авторизации и регистрации в системе
    /// </summary>
    public class AccountModel
    {
        /// <summary>
        /// Почта пользователя
        /// </summary>
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        public string CustomUserName { get; set; }

        /// <summary>
        /// Почта пользователя
        /// </summary>
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        [EmailAddress(ErrorMessage = "Е-mail введен некорректно!")]
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
