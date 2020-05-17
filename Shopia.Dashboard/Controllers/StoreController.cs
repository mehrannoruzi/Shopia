using Elk.AspNetCore;
using Elk.Core;
using System.Linq;
using Shopia.Domain;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Shopia.Dashboard.Resources;

namespace Shopia.Dashboard.Controllers
{
    public class StoreController : Controller
    {
        readonly IConfiguration _configuration;
        public StoreController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            ViewBag.Types = EnumExtension.GetEnumElements<StoreType>().Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = x.Description
            }).ToList();
            return View(new StoreSignUpModel());
        }


        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult SignUp(StoreSignUpModel model)
        {
            if (!ModelState.IsValid) return Json(new Response<string> { IsSuccessful = false, Message = ModelState.GetModelError() });

            return Json(new { IsSuccessful = true });
        }

        [HttpGet]
        public async Task<IActionResult> ShowPage(StoreType storeType, string username)
        {
            using (var http = new HttpClient())
            {
                var url = $"{_configuration["CustomSettings:Crawler:GetPage"]}?username={username}";
                var apiCall = await http.GetAsync(url);
                if (!apiCall.IsSuccessStatusCode) return Json(new Response<string> { Message = Strings.Error });
                var response = await apiCall.Content.ReadAsStringAsync();
                var crawl = response.DeSerializeJson<Response<CrawledPageDto>>();
                return Json(new Response<string>
                {
                    IsSuccessful = crawl.IsSuccessful,
                    Message = crawl.Message,
                    Result = ControllerExtension.RenderViewToString(this, "Partials/_Instagram", crawl.Result),
                });

            }
        }
    }
}