using Microsoft.AspNetCore.Mvc;
using Shopia.Domain;
using Shopia.Service;
using System.Threading.Tasks;

namespace Shopia.Store.Api.Controllers
{
    public class ProductController : Controller
    {
        readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(ProductFilterDTO filter)=> Json(await _productService.Get(filter));

        [HttpGet]
        public async Task<IActionResult> GetSingle(int id) => Json(await _productService.FindAsDtoAsync(id));
    }
}