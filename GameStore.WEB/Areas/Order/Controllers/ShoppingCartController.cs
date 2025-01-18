using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WEB.Areas.Order.Controllers
{
    [Area("Order")]
    [Authorize]
    public class ShoppingCartController : Controller
    {
        public IActionResult GetShoppingCart()
        {
            return View();
        }
    }
}
