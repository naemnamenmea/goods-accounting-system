using GoodsAccountingSystem.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodsAccountingSystem.Controllers
{
    [Authorize(Roles = Role.ADMIN)]
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
