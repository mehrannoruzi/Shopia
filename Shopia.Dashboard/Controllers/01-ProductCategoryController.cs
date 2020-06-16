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
using Microsoft.AspNetCore.Authorization;

namespace Shopia.Dashboard.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryService _productCatSrv;

        public ProductCategoryController(IProductCategoryService productCategorySrv)
        {
            _productCatSrv = productCategorySrv;
        }

        [NonAction]
        private void GetAddPartial() => ViewBag.EntityPartial = ControllerExtension.RenderViewToString(this, "Partials/_Entity", new ProductCategory());

        [HttpGet]
        public virtual ActionResult Manage(ProductCategorySearchFilter filter)
        {
            GetAddPartial();
            ViewBag.EntityPartial = ControllerExtension.RenderViewToString(this, "Partials/_Entity", new ProductCategory());
            return View(_productCatSrv.GetAll(filter));
        }



        [HttpPost]
        public virtual async Task<JsonResult> Add(ProductCategory model)
        {
            GetAddPartial();
            if (!ModelState.IsValid) return Json(new Response<string> { IsSuccessful = false, Message = ModelState.GetModelError() });
            var add = await _productCatSrv.AddAsync(model);
            if (!add.IsSuccessful) return Json(add);
            return Json(new Response<string>
            {
                IsSuccessful = true,
                Result = await ControllerExtension.RenderViewToStringAsync(this, "Partials/_List", _productCatSrv.GetAll(new ProductCategorySearchFilter()))
            });
        }

        [HttpGet]
        public virtual async Task<JsonResult> Update(int id)
        {
            var findProduct = await _productCatSrv.FindAsync(id);
            if (!findProduct.IsSuccessful) return Json(new Response<string> { IsSuccessful = false, Message = Strings.RecordNotFound.Fill(DomainString.Product) });
            return Json(new Response<string>
            {
                IsSuccessful = true,
                Result = await ControllerExtension.RenderViewToStringAsync(this, "Partials/_Entity", findProduct.Result)
            });
        }

        [HttpPost]
        public virtual async Task<JsonResult> Update(ProductCategory model)
        {
            GetAddPartial();
            if (!ModelState.IsValid) return Json(new Response<string> { IsSuccessful = false, Message = ModelState.GetModelError() });
            var update = await _productCatSrv.UpdateAsync(model);
            if (!update.IsSuccessful)
                return Json(update);
            return Json(new
            {
                IsSuccessful = true,
                Result = await ControllerExtension.RenderViewToStringAsync(this, "Partials/_List", _productCatSrv.GetAll(new ProductCategorySearchFilter()))
            });
        }

        [HttpPost]
        public virtual async Task<JsonResult> Delete(int id)
        {
            var delete = await _productCatSrv.DeleteAsync(id);
            if (!delete.IsSuccessful)
                return Json(delete);
            return Json(new
            {
                IsSuccessful = true,
                Result = await ControllerExtension.RenderViewToStringAsync(this, "Partials/_List", _productCatSrv.GetAll(new ProductCategorySearchFilter()))
            });
        }


        //[HttpGet]
        //public virtual ActionResult Manage(ProductSearchFilter filter)
        //{
        //    if (!Request.IsAjaxRequest()) return View(_productSrv.Get(filter));
        //    else return PartialView("Partials/_List", _productSrv.Get(filter));
        //}

        [HttpGet, AllowAnonymous]
        public virtual JsonResult Search(string q)
            => Json(_productCatSrv.Search(q).ToSelectListItems());
    }
}