using Microsoft.AspNetCore.Mvc;

namespace Shopia.Store.Api.Controllers
{
    public class StoreController : Controller
    {
        //sample api
        public IActionResult GetSingle(int id)
        {
            return Json(new
            {
                IsSuccessful = true,
                Result = new
                {
                    name = "شاپیا | Shopia",
                    desc = "Shopia & Retail",
                    accountText = "حساب رسمی شاپیا",
                    deliveryText = "ارسال 0 تا 100 کالا با ما",
                    gatewayText = "تخصیص درگاه بانکی مستقل به فروشندگان",
                    logoUrl = "https://www.instagram.com/static/images/ico/apple-touch-icon-120x120-precomposed.png/8a5bd3f267b1.png",
                    followersCount = "3k",
                    postsCount = "500",
                    followingCount = "100",
                }
            });
        }
    }
}