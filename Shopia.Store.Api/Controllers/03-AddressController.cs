using System;
using Elk.Core;
using Shopia.Domain;
using Shopia.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Shopia.Store.Api.Resources;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Linq;

namespace Shopia.Store.Api.Controllers
{
    public class AddressController : Controller
    {
        readonly IConfiguration _configuration;
        readonly IAddressService _addressService;
        readonly IDeliveryService _deliverySrv;
        public AddressController(IAddressService addressService, IDeliveryService deliverySrv, IConfiguration configuration)
        {
            _addressService = addressService;
            _deliverySrv = deliverySrv;
            _configuration = configuration;
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
        public async Task<IActionResult> GetDeliveryCost(int storeId, LocationDTO location)
                => Json(await _deliverySrv.GetDeliveryTypes(storeId, location));
    }
}