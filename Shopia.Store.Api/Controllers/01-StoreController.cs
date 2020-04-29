using Microsoft.AspNetCore.Mvc;

namespace Shopia.Store.Api.Controllers
{
    public class StoreController : Controller
    {
        [HttpGet]
        public IActionResult GetSingle(int id)
        {
            return Json(new
            {
                IsSuccessful = true,
                Result = new StoreDTO
                {
                    Name = "شاپیا | Shopia",
                    LogoUrl = "https://www.instagram.com/static/images/ico/apple-touch-icon-120x120-precomposed.png/8a5bd3f267b1.png",
                }
            });
        }
    }
}