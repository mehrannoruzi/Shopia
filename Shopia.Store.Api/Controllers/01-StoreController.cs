using Microsoft.AspNetCore.Mvc;
using Shopia.Service;
using System.Threading.Tasks;

namespace Shopia.Store.Api.Controllers
{
    public class StoreController : Controller
    {
        readonly IStoreService _storeService;
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Json(await _storeService.FindAsync(id));
        }
    }
}