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
        public async Task<IActionResult> Price(DeliveryType type)
            => Ok(await _deliveryService.InquiryDeliveryPriceAsync(type));
    }
}