using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GameStore.DAL.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        /// <summary>
        /// Email пользователя
        /// </summary>
        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        [EmailAddress(ErrorMessage = "Е-mail введен некорректно!")]
        public override string? Email { get; set; }

        /// <summary>
        /// Аватарка пользователя - изоображение
        /// </summary>
        public byte[]? AvatarImage { get; set; }
       
        /// <summary>
        /// Аватарка пользователя - имя картинки
        /// </summary>
        public string? AvatarName { get; set; }

        /// <summary>
        /// Последнее посещение
        /// </summary>
        public DateTime? LastVisit { get; set; }

        //Технические поля
        /// <summary>
        /// Роль пользователя - контролируется в ручную (чтобы избежать лишних действий для отображения роли пользователя в таблице)
        /// </summary>
        public string? UserRoleName { get; set; }

        /// <summary>
        /// Пользовательский Id - нужен для отображения в области Админ (чтобы не видеть guid)
        /// </summary>
        public long CustomUserId { get; set; }

        /// <summary>
        /// Корзина
        /// </summary>
        public List<ShoppingCart> ShoppingCarts { get; set; } = new();
    }
}
