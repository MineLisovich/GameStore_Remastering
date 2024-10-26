using GameStore.BLL.Infrastrcture.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = IdentityUserPolicy.role_adminOnly)]
    public class GamesManagerController : Controller
    {
        #region PUBLIC METHODS - GET
        [HttpGet]
        public IActionResult GetGamesList()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GameCreateEditForm()
        {
            return View();
        }

        #endregion

        #region PUBLIC METHODS - POST
        #endregion

        #region PRIVATE METHODS
        #endregion

        #region  PRIVATE METHODS - TEMPDATA
        #endregion
    }
}
