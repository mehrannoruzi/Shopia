using Elk.Core;
using Shopia.Domain;
using Shopia.Service;
using Elk.Http;
using Elk.AspNetCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shopia.Dashboard.Resources;
using DomainString = Shopia.Domain.Resource.Strings;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Shopia.Dashboard.Controllers
{
    // [AuthorizationFilter]
    public partial class StoreOrderController : Controller
    {
        private readonly IOrderService _OrderSrv;
        private readonly IStoreService _storeSrv;

        public StoreOrderController(IOrderService OrderSrv, IStoreService storeSrv)
        {
            _OrderSrv = OrderSrv;
            _storeSrv = storeSrv;
        }

        [NonAction]
        private IEnumerable<SelectListItem> GetStores() => _storeSrv.GetAll(User.GetUserId()).Select(x => new SelectListItem
        {
            Text = x.FullName,
            Value = x.StoreId.ToString()
        }).ToList();

        //[HttpGet]
        //public virtual JsonResult Add()
        //    => Json(new Modal
        //    {
        //        Title = $"{Strings.Add} {DomainString.Order}",
        //        Body = ControllerExtension.RenderViewToString(this, "Partials/_Entity", new Order()),
        //        AutoSubmitUrl = Url.Action("Add", "Order")
        //    });

        //[HttpPost]
        //public virtual async Task<JsonResult> Add(Order model)
        //{
        //    if (!ModelState.IsValid) return Json(new { IsSuccessful = false, Message = ModelState.GetModelError() });
        //    return Json(await _OrderSrv.AddAsync(model));
        //}

        [HttpGet]
        public virtual async Task<JsonResult> Update(int id)
        {
            var chk = await _OrderSrv.CheckOwner(User.GetUserId(), id);
            if (!chk) return Json(new { IsSuccessful = false, Message = Strings.RecordNotFound });
            var findRep = await _OrderSrv.FindAsync(id);
            if (!findRep.IsSuccessful) return Json(new { IsSuccessful = false, Message = Strings.RecordNotFound.Fill(DomainString.Order) });
            return Json(new Modal
            {
                Title = $"{Strings.Update} {DomainString.Order}",
                AutoSubmitBtnText = Strings.Edit,
                Body = ControllerExtension.RenderViewToString(this, "Partials/_Entity", findRep.Result),
                AutoSubmitUrl = Url.Action("Update", "Order"),
                ResetForm = false
            });
        }

        [HttpPost]
        public virtual async Task<JsonResult> Update(Order model)
        {
            var chk = await _OrderSrv.CheckOwner(User.GetUserId(), model.OrderId);
            if (!chk) return Json(new { IsSuccessful = false, Message = Strings.RecordNotFound });
            if (!ModelState.IsValid) return Json(new { IsSuccessful = false, Message = ModelState.GetModelError() });
            return Json(await _OrderSrv.UpdateStatusAsync(model.OrderId, model.OrderStatus));
        }

        [HttpGet, AuthEqualTo("StoreOrder", "Update")]
        public virtual async Task<JsonResult> Details(int id)
        {
            var findRep = await _OrderSrv.FindAsync(id);
            if (!findRep.IsSuccessful) return Json(new { IsSuccessful = false, Message = Strings.RecordNotFound.Fill(DomainString.Order) });

            return Json(new Modal
            {
                Title = $"{Strings.Details} {DomainString.Order}",
                AutoSubmitBtnText = Strings.Edit,
                Body = ControllerExtension.RenderViewToString(this, "Partials/_Details", findRep.Result),
                AutoSubmit = false
            });
        }

        [HttpPost]
        public virtual async Task<JsonResult> Delete(int id) => Json(await _OrderSrv.DeleteAsync(id));

        [HttpGet]
        public virtual ActionResult Manage(OrderSearchFilter filter)
        {
            ViewBag.WithoutAddButton = true;
            ViewBag.Stores = GetStores();
            if (!Request.IsAjaxRequest()) return View(_OrderSrv.Get(filter));
            else return PartialView("Partials/_List", _OrderSrv.Get(filter));
        }

    }
}