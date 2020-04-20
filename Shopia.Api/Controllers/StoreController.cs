using Shopia.Service;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Shopia.Api.Controllers
{
    public class StoreController : Controller
    {
        private StoreService _storeService;

        public StoreController(StoreService storeService)
        {
            _storeService = storeService;
        }


        [HttpPost]
        public async Task<IActionResult> SuccessCrawlAsync(string uniqueId)
            => Ok(await _storeService.SuccessCrawlAsync(uniqueId));


    }
}