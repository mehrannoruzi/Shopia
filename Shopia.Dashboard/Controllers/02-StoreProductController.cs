using Elk.Core;
using Elk.Http;
using System;
using System.Linq;
using Shopia.Domain;
using Elk.AspNetCore;
using Shopia.Service;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shopia.Dashboard.Resources;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Shopia.Dashboard.Controllers
{
    public class StoreProductController : Controller
    {
        readonly IProductService _productSerive;
        readonly IConfiguration _configuration;
        public StoreProductController(IProductService productSerive, IConfiguration configuration)
        {
            _productSerive = productSerive;
            _configuration = configuration;
        }

        public IActionResult Manage([FromServices]IStoreService storeSerive, ProductSearchFilter filter)
        {
            ViewBag.Stores = storeSerive.GetAll(User.GetUserId());
            if (!Request.IsAjaxRequest()) return View(_productSerive.Get(filter));
            else return PartialView("Partials/_List", _productSerive.Get(filter));
        }

        [HttpGet]
        public async Task<IActionResult> Post(string username, int pageNumber)
        {
            using var getPostsHttp = new HttpClient();
            var apiCall = await getPostsHttp.GetAsync($"{_configuration["CustomSettings:Crawler:GetPosts"]}?pageSize=6&username={username}&pageNumber={pageNumber}");
            if (!apiCall.IsSuccessStatusCode) return Json(new { IsSuccessful = false, Message = Strings.Error });
            var getPosts = (await apiCall.Content.ReadAsStringAsync()).DeSerializeJson<Response<List<Post>>>();
            if (!getPosts.IsSuccessful) return Json(new { IsSuccessful = false, getPosts.Message });
            var model = getPosts.Result.Select(x => new PostModel
            {
                Description = x.Description,
                UniqueId = x.UniqueId,
                Assets = x.AssetList
            }).ToList();
            return Json(new
            {
                IsSuccessful = true,
                Result = new
                {
                    Partial = this.RenderViewToString("~/Views/StoreProduct/Partials/_Posts.cshtml", model),
                    Posts = model
                }
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddRange(int storeId, IList<PostModel> posts)
            => Json(await _productSerive.AddRangeAsync(new ProductAddModel
            {
                Posts = posts,
                StoreId = storeId
            }));
    }
}