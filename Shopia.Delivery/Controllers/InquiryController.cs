using Shopia.Domain;
using System.Threading.Tasks;
using Shopia.Delivery.Service;
using Microsoft.AspNetCore.Mvc;

namespace Shopia.Delivery.Controllers
{
    public class InquiryController : Controller
    {
        public IDeliveryService _deliveryService { get; }

        public InquiryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }


        [HttpGet]
        public IActionResult Index()
            => Ok("WellCome To Shopia.Delivery Api ...");


        [HttpGet]
        public async Task<IActionResult> Address(LocationDTO location)
            => Ok(await _deliveryService.AddressInquiry(location));


        [HttpPost]
        public async Task<IActionResult> Price([FromBody]LocationsDTO priceInquiry)
            => Ok(await _deliveryService.PriceInquiry(priceInquiry, false, false));
        
    }
}