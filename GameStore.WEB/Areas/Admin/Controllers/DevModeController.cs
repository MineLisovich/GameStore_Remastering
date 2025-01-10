using GameStore.BLL.Infrastrcture;
using GameStore.BLL.Infrastrcture.Identity;
using GameStore.BLL.Services.DevModeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = IdentityUserPolicy.role_adminOnly)]
    public class DevModeController : Controller
    {
        private readonly IAddPdGame _addPdGame;

        public DevModeController(IAddPdGame addPdGame)
        {
            _addPdGame = addPdGame;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> AddGames()
        {
            ResultServiceModel result = await _addPdGame.AddPdGames();
            return Json(result);
        }
    }
}
