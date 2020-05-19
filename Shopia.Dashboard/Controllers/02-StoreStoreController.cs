using Microsoft.AspNetCore.Mvc;

namespace Shopia.Dashboard.Controllers
{
    public class StoreStoreController : Controller
    {
        public IActionResult Manage()
        {
            return View();
        }
    }
}