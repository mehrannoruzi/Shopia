using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Shopia.Store.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult ContactUs()
        {
            return Json(new
            {
                IsSuccessful = true,
                Result = new
                {
                    WhatsappLink = "https://wa.me/989116107197",
                    TelegramLink = "https://t.me/kingofday",
                    PhoneNumbers = new List<string> { "9334188188", "933561109" },
                    WebsiteName = "shopia.ir",
                    WebsiteUrl = "https://kingofday.ir"
                }
            });
        }
    }
}
