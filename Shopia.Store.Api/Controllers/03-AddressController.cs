using System;
using Elk.Core;
using Shopia.Domain;
using Shopia.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Shopia.Store.Api.Resources;

namespace Shopia.Store.Api.Controllers
{
    public class AddressController : Controller
    {
        readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            if (!Guid.TryParse(Request.Headers["token"], out Guid userId))
                return Json(new Response<List<AddressDTO>>
                {
                    IsSuccessful = false,
                    Message = Strings.ThereIsNoToken
                });
            return Json(_addressService.Get(userId));
        }

        [HttpGet]
        public IActionResult GetDeliveryCost(LocationDTO location)
        {
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