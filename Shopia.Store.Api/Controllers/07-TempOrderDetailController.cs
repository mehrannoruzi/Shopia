using System;
using Shopia.Service;
using Microsoft.AspNetCore.Mvc;

namespace Shopia.Store.Api.Controllers
{
    public class TempOrderDetailController : Controller
    {
        private readonly ITempOrderDetailService _tempOrderDetailSrv;
        public TempOrderDetailController(ITempOrderDetailService tempOrderDetailSrv)
        {
            _tempOrderDetailSrv = tempOrderDetailSrv;
        }

        [HttpGet]
        public IActionResult Get(Guid basketId) => Json(_tempOrderDetailSrv.Get(basketId));
    }
}