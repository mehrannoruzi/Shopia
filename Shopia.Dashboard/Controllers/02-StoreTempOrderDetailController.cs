using System;
using Elk.Http;
using Elk.Core;
using Shopia.Domain;
using Shopia.Service;
using Elk.AspNetCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shopia.Dashboard.Resources;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DomainString = Shopia.Domain.Resource.Strings;

namespace Shopia.Dashboard.Controllers
{
    [AuthorizationFilter]
    public partial class StoreTempOrderDetailController : Controller
    {
        private readonly ITempOrderDetailService _TempOrderDetailSrv;
        private readonly IConfiguration _configuration;

        public StoreTempOrderDetailController(ITempOrderDetailService TempOrderDetailSrv, IConfiguration configuration)
        {
            _TempOrderDetailSrv = TempOrderDetailSrv;
            _configuration = configuration;
        }


        [HttpGet]
        public virtual JsonResult Add()
            => Json(new Modal
            {
                Title = $"{Strings.Add} {DomainString.Basket}",
                Body = ControllerExtension.RenderViewToString(this, "Partials/_Entity", new TempOrderDetail()),
                AutoSubmit = false
            });

        [HttpPost]
        public virtual async Task<JsonResult> Add([FromBody]IList<TempOrderDetail> items)
        {
            if (items == null || items.Count == 0) return Json(new { IsSuccessful = false, Message = Strings.ThereIsNoRecord });
            if (!ModelState.IsValid) return Json(new { IsSuccessful = false, Message = ModelState.GetModelError() });
            var add = await _TempOrderDetailSrv.AddRangeAsync(items);
            if (!add.IsSuccessful) return Json(add);
            var url = $"{_configuration["CustomSettings:ReactTempBasketUrl"]}/{add.Result}";
            return Json(new Response<string> { IsSuccessful = true, Result = await ControllerExtension.RenderViewToStringAsync(this, "Partials/_Result", (object)url) });
        }

        [HttpGet, AuthEqualTo("StoreTempOrderDetail", "Add")]
        public virtual JsonResult Details(Guid id)
        {
            ViewBag.BasketUrl = $"{_configuration["CustomSettings:ReactTempBasketUrl"]}/{id}";
            return Json(new Modal
            {
                Title = $"{Strings.Details} {DomainString.Basket}",
                AutoSubmitBtnText = Strings.Edit,
                Body = ControllerExtension.RenderViewToString(this, "Partials/_Details", _TempOrderDetailSrv.Get(id)),
                AutoSubmit = false
            });
        }

        //[HttpPost]
        //public virtual async Task<JsonResult> Update(TempOrderDetail model)
        //{
        //    if (!ModelState.IsValid) return Json(new { IsSuccessful = false, Message = ModelState.GetModelError() });
        //    return Json(await _TempOrderDetailSrv.UpdateAsync(model));
        //}

        [HttpPost]
        public virtual async Task<JsonResult> Delete(Guid id) => Json(await _TempOrderDetailSrv.DeleteAsync(id));

        [HttpGet]
        public virtual ActionResult Manage(TempOrderDetailSearchFilter filter)
        {
            if (!Request.IsAjaxRequest()) return View(_TempOrderDetailSrv.Get(filter));
            else return PartialView("Partials/_List", _TempOrderDetailSrv.Get(filter));
        }

    }
}