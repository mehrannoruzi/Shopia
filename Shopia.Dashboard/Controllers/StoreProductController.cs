using Microsoft.AspNetCore.Mvc;
using Shopia.Domain;
using Shopia.Service;
using System;
using System.Collections.Generic;

namespace Shopia.Dashboard.Controllers
{
    public class StoreProductController : Controller
    {
        readonly IStoreService _storeSerive;
        public StoreProductController(IStoreService storeSerive)
        {
            _storeSerive = storeSerive;
        }

        public IActionResult Manage()
        {
            ViewBag.Stores = _storeSerive.GetAll(Guid.Parse(User.Identity.Name));
            return View();
        }

        [HttpGet]
        public IActionResult Post()
        {
            //var 
            return PartialView(new List<Post> {new Post{
                ViewCount = 10,
                LikeCount = 15,
                
             } });
        }
    }
}