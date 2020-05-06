using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Shopia.Store.Api.Controllers
{
    public class AddressController : Controller
    {
        //سه آدرس آخر ثبت شده برای کاربر
        [HttpGet]
        public IActionResult Get()
        {
            var token = Request.Headers["token"];
            return Json(new
            {
                IsSuccessful = true,
                Result = new List<AddressDTO> {
                    new AddressDTO
                    {
                        Id = 1,
                        Address="میدان کاج، انتهای خیابان زحمت، کوچه توحید، پلاک 4، طبفه اول، واحد 2",
                        Lat = 35.781973,
                        Lng = 51.374830
                    },
                    new AddressDTO
                    {
                        Id = 2,
                        Address="میدان توحید، انتهای خیابان الوند، کوچه نرگس، پلاک 43، طبفه دوم، واحد 12",
                        Lat = 35.781973,
                        Lng = 51.374830
                    }
                }
            });
        }

        [HttpGet]
        public IActionResult GetDeliveryCost(LocationDTO location)
        {
            var token = Request.Headers["token"];
            return Json(new
            {
                IsSuccessful = true,
                Result = new
                {
                    PlaceName = "تهران بریانک",
                    Items = new List<DeliveryDto>
                        {
                            new DeliveryDto
                            {
                                Id =1,
                                Name = "پست",
                                Cost = 5000
                            },
                             new DeliveryDto
                            {
                                Id =2,
                                Name = "پیک",
                                Cost = 15000
                            }
                        }
                }
            });
        }
    }
}