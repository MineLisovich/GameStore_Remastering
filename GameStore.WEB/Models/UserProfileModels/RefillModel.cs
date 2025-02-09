using System.ComponentModel.DataAnnotations;

namespace GameStore.WEB.Models.UserProfileModels
{
    public class RefillModel
    {
        [Required(ErrorMessage = "Пожалуйста, введите 16-значный номер карты")]
        [RegularExpression(@"^\d{4} \d{4} \d{4} \d{4}$", ErrorMessage = "Номер карты должен быть в формате 9999 9999 9999 9999.")]
        [MaxLength(19)]
        public string CardNum { get; set; }
       
        [Required(ErrorMessage = "Это поля обязательно к заполнению!")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "Формат: MM/ГГ")]
        [MaxLength(5)]
        public string ValidDate { get; set; }

        [Required(ErrorMessage = "Это поле обязательно к заполнению!")]
        [MaxLength(3)]
        public string CVV { get; set; }

        [Required(ErrorMessage = "Это поле обязательно к заполнению!")]
        public string CardName { get; set; }

        [Required(ErrorMessage = "Это поле обязательно к заполнению!")]
        public decimal? PaymentSum { get; set; } = null;
    }
}
