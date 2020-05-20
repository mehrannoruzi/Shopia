﻿using Elk.Core;
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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using DomainString = Shopia.Domain.Resource.Strings;

namespace Shopia.Dashboard.Controllers
{
    public class StoreProductController : Controller
    {
        readonly IProductService _productSerive;
        readonly IProductCategoryService _productCategorySerive;
        readonly IConfiguration _configuration;
        public StoreProductController(IProductService productSerive, IProductCategoryService productCategorySerive, IConfiguration configuration)
        {
            _productSerive = productSerive;
            _productCategorySerive = productCategorySerive;
            _configuration = configuration;
        }

        [NonAction]
        private List<SelectListItem> GetCategories()
        {
            var categories = _productCategorySerive.Get(new ProductCategorySearchFilter());
            if (categories.Items == null) return new List<SelectListItem>();
            return categories.Items.Select(x => new SelectListItem
            {
                Value = x.ProductCategoryId.ToString(),
                Text = x.Name
            }).ToList();
        }
        public IActionResult Manage([FromServices]IStoreService storeSerive, ProductSearchFilter filter)
        {
            var userId = User.GetUserId();
            ViewBag.Stores = storeSerive.GetAll(userId);
            filter.UserId = userId;
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

        [HttpGet]
        public virtual async Task<JsonResult> Update(int id)
        {
            var findRep = await _productSerive.FindAsync(id);
            if (!findRep.IsSuccessful) return Json(new { IsSuccessful = false, Message = Strings.NotFound });
            ViewBag.Categories = GetCategories();
            return Json(new Modal
            {
                Title = $"{Strings.Update} {DomainString.Product}",
                AutoSubmitBtnText = Strings.Edit,
                Body = ControllerExtension.RenderViewToString(this, "Partials/_Entity", findRep.Result),
                AutoSubmit = false
            });
        }

        [HttpPost]
        public virtual async Task<JsonResult> Update(Product model)
        {
            if (!ModelState.IsValid) return Json(new { IsSuccessful = false, Message = ModelState.GetModelError() });
            return Json(await _productSerive.UpdateAsync(model));
        }
    }
}