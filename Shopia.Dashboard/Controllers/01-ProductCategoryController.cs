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
        private readonly IProductCategoryService _productSrv;

        public ProductCategoryController(IProductCategoryService productCategorySrv)
        {
            _productSrv = productCategorySrv;
        }

        [HttpGet]
        public virtual ActionResult Manage(ProductCategorySearchFilter filter)
        {
            return View(_productSrv.GetAll(filter));
        }


        //[HttpGet]
        //public virtual JsonResult Add()
        //    => Json(new Modal
        //    {
        //        Title = $"{Strings.Add} {DomainString.Product}",
        //        Body = ControllerExtension.RenderViewToString(this, "Partials/_Entity", new Product()),
        //        AutoSubmitUrl = Url.Action("Add", "Product")
        //    });

        //[HttpPost]
        //public virtual async Task<JsonResult> Add(Product model)
        //{
        //    if (!ModelState.IsValid) return Json(new Response<string> { IsSuccessful = false, Message = ModelState.GetModelError() });
        //    return Json(await _productSrv.AddAsync(model));
        //}

        //[HttpGet]
        //public virtual async Task<JsonResult> Update(int id)
        //{
        //    var findProduct = await _productSrv.FindAsync(id);
        //    if (!findProduct.IsSuccessful) return Json(new Response<string> { IsSuccessful = false, Message = Strings.RecordNotFound.Fill(DomainString.Product) });
        //    return Json(new Modal
        //    {
        //        Title = $"{Strings.Update} {DomainString.Product}",
        //        AutoSubmitBtnText = Strings.Edit,
        //        Body = await ControllerExtension.RenderViewToStringAsync(this, "Partials/_Entity", findProduct.Result),
        //        AutoSubmitUrl = Url.Action("Update", "Product"),
        //        ResetForm = false
        //    });
        //}

        //[HttpPost]
        //public virtual async Task<JsonResult> Update(Product model)
        //{
        //    if (!ModelState.IsValid) return Json(new Response<string> { IsSuccessful = false, Message = ModelState.GetModelError() });
        //    return Json(await _productSrv.UpdateAsync(model));
        //}

        //[HttpPost]
        //public virtual async Task<JsonResult> Delete(int id) => Json(await _productSrv.DeleteAsync(id));


        //[HttpGet]
        //public virtual ActionResult Manage(ProductSearchFilter filter)
        //{
        //    if (!Request.IsAjaxRequest()) return View(_productSrv.Get(filter));
        //    else return PartialView("Partials/_List", _productSrv.Get(filter));
        //}

        [HttpGet, AllowAnonymous]
        public virtual JsonResult Search(string q)
            => Json(_productSrv.Search(q).ToSelectListItems());
    }
}