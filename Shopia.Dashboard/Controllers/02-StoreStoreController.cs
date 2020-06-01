using Elk.Core;
using Elk.Http;
using Shopia.Domain;
using Shopia.Service;
using Elk.AspNetCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shopia.Dashboard.Resources;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using DomainString = Shopia.Domain.Resource.Strings;

namespace Shopia.Dashboard.Controllers
{
    [AuthorizationFilter]
    public class StoreStoreController : Controller
    {
        private readonly IStoreService _storeSrv;
        readonly IConfiguration _configuration;
        public StoreStoreController(IStoreService storeSrv, IConfiguration configuration)
        {
            _storeSrv = storeSrv;
            _configuration = configuration;
        }


        [HttpGet]
        public virtual async Task<JsonResult> Update([FromServices]IAddressService addrSrv, int id)
        {
            var store = await _storeSrv.FindAsync(id);
            if (!store.IsSuccessful) return Json(new { IsSuccessful = false, Message = Strings.RecordNotFound });
            var model = new StoreUpdateModel().CopyFrom(store.Result);
            if (store.Result.AddressId != null)
            {
                var addr = await addrSrv.FindAsync(store.Result.AddressId ?? 0);
                if (addr.IsSuccessful)
                {
                    model.Latitude = addr.Result.Latitude;
                    model.Longitude = addr.Result.Longitude;
                    model.AddressDetails = addr.Result.AddressDetails;
                }
            }
            return Json(new Modal
            {
                Title = $"{Strings.Update} {DomainString.Store}",
                AutoSubmitBtnText = Strings.Edit,
                Body = ControllerExtension.RenderViewToString(this, "Partials/_Entity", model),
                AutoSubmit = false
            });
        }

        [HttpPost]
        public virtual async Task<JsonResult> Update([FromServices]IWebHostEnvironment env, StoreUpdateModel model)
        {
            if (!ModelState.IsValid) return Json(new { IsSuccessful = false, Message = ModelState.GetModelError() });
            model.Root = env.WebRootPath;
            model.BaseDomain = _configuration["CustomSettings:BaseDomain"];
            return Json(await _storeSrv.UpdateAsync(model));
        }

        [HttpPost]
        public virtual async Task<JsonResult> Delete(int id) => Json(await _storeSrv.DeleteAsync(id));

        [HttpGet]
        public virtual ActionResult Manage(StoreSearchFilter filter)
        {
            if (!Request.IsAjaxRequest()) return View(_storeSrv.Get(filter));
            else return PartialView("Partials/_List", _storeSrv.Get(filter));
        }

        [HttpPost, AuthEqualTo("StoreStore", "Update")]
        public virtual async Task<IActionResult> DeleteLogo([FromServices]IWebHostEnvironment env, int id) => Json(await _storeSrv.DeleteFile(_configuration["CustomSettings:BaseDomain"], env.WebRootPath, id));

        [HttpGet, AuthEqualTo("StoreStore", "Update")]
        public virtual JsonResult Search(string q)
            => Json(_storeSrv.Search(q, User.GetUserId()).ToSelectListItems());
    }
}