using Elk.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopia.Service;
using System;

namespace Shopia.Dashboard.Controllers
{
    public class StoreController : Controller
    {
        readonly IStoreService _storeSrv;
        public StoreController(IStoreService storeSrv)
        {
            _storeSrv = storeSrv;
        }

        [HttpGet, AllowAnonymous]
        public virtual JsonResult Search(string q, Guid? userId)
        => Json(_storeSrv.Search(q, userId).ToSelectListItems());


    }
}