using Microsoft.AspNetCore.Mvc;

namespace Shopia.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Wellcome To Shopia Api...");
        }

    }
}
