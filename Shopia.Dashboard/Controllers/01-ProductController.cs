using System;
using Elk.Core;
using Shopia.Domain;
using Shopia.Service;
using Elk.AspNetCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shopia.Dashboard.Resources;
using DomainString = Shopia.Domain.Resource.Strings;
using Elk.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Shopia.Dashboard.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productSrv;
        readonly IConfiguration _configuration;

        public ProductController(IProductService productSrv, IConfiguration configuration)
        {
            _productSrv = productSrv;
            _configuration = configuration;
        }


        [HttpGet]
        public virtual JsonResult Add()
            => Json(new Modal
            {
                Title = $"{Strings.Add} {DomainString.Product}",
                Body = ControllerExtension.RenderViewToString(this, "Partials/_Entity", new Product()),
                AutoSubmitUrl = Url.Action("Add", "Product")
            });

        [HttpPost]
        public virtual async Task<JsonResult> Add([FromServices]IWebHostEnvironment env, ProductAddModel model)
        {
            if (!ModelState.IsValid) return Json(new Response<string> { IsSuccessful = false, Message = ModelState.GetModelError() });
            model.BaseDomain = _configuration["CustomSettings:BaseDomain"];
            model.Root = env.WebRootPath;
            return Json(await _productSrv.AddAsync(model));
        }

        [HttpGet]
        public virtual async Task<JsonResult> Update(int id)
        {
            var findProduct = await _productSrv.FindAsDtoAsync(id);
            if (!findProduct.IsSuccessful) return Json(new Response<string> { IsSuccessful = false, Message = Strings.RecordNotFound.Fill(DomainString.Product) });
            return Json(new Modal
            {
                Title = $"{Strings.Update} {DomainString.Product}",
                AutoSubmitBtnText = Strings.Edit,
                Body = await ControllerExtension.RenderViewToStringAsync(this, "Partials/_Entity", findProduct.Result),
                AutoSubmitUrl = Url.Action("Update", "Product"),
                ResetForm = false
            });
        }

        [HttpPost]
        public virtual async Task<JsonResult> Update([FromServices]IWebHostEnvironment env, ProductAddModel model)
        {
            if (!ModelState.IsValid) return Json(new Response<string> { IsSuccessful = false, Message = ModelState.GetModelError() });
            model.BaseDomain = _configuration["CustomSettings:BaseDomain"];
            model.Root = env.WebRootPath;
            return Json(await _productSrv.UpdateAsync(model));
        }

        [HttpPost]
        public virtual async Task<JsonResult> Delete([FromServices]IWebHostEnvironment env, int id) => Json(await _productSrv.DeleteAsync(_configuration["CustomSettings:BaseDomain"], env.WebRootPath, id));


        [HttpGet]
        public virtual ActionResult Manage(ProductSearchFilter filter)
        {
            if (!Request.IsAjaxRequest()) return View(_productSrv.Get(filter));
            else return PartialView("Partials/_List", _productSrv.Get(filter));
        }

        //[HttpGet, AuthEqualTo("ProductInRole", "Add")]
        //public virtual JsonResult Search(string q)
        //    => Json(_productSrv.Search(q).ToSelectListItems());
    }
}