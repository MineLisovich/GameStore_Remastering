using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace GameStore.WEB.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {

        #region PUBLIC METHODS - GET
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Catalog()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Discounts()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Support()
        {
            return View();
        }
        #endregion
    }
}
