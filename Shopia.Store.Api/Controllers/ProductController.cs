using Microsoft.AspNetCore.Mvc;
using Shopia.Domain;
using System.Collections.Generic;

namespace Shopia.Store.Api.Controllers
{
    public class ProductController : Controller
    {
        //sample api
        public IActionResult Get(int storeId, int pageNumber, int pageSize)
        {
            return Json(new
            {
                IsSuccessful = true,
                Result=new List<Product>
                {
                    //name: 'اتو مو',
                    //likeCount: 20,
                    //price: 50000,
                    //count:3,
                    //imgUrl: 'https://storage.torob.com/backend-api/base/images/Ta/hO/TahOLrRj5RRh9W9n.jpg'
                }
            });
        }
    }
}