using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GameStore.WEB.Areas.Order.Models
{
    public class PaymentMethodsModel
    {
        public long ShoppingCartId { get; set; }
        public decimal TotalPrice { get; set; }

        [Required(ErrorMessage ="Выберите способ оплаты")]
        public int PaymentMethodId {  get; set; }

        public SelectList SelectItemsPaymentMethods { get; set; }
    }
}
