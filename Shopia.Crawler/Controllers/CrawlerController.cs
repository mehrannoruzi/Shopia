using Elk.Core;
using Shopia.Crawler.Service;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Shopia.Crawler.Controllers
{
    public class CrawlerController : Controller
    {
        public ICrawlerService _crawlerService { get; }

        public CrawlerController(ICrawlerService crawlerService)
        {
            _crawlerService = crawlerService;
        }


        [HttpGet]
        public IActionResult Index()
            => Ok("WellCome To Shopia.Crawler Api ...");

        [HttpGet]
        public async Task<IActionResult> PageAsync(string Username)
            => Ok(await _crawlerService.CrawlPageAsync(Username));

        [HttpPost]
        public async Task<IActionResult> PostAsync(string Username)
            => Ok(await _crawlerService.CrawlPostAsync(Username));

        [HttpGet]
        public async Task<IActionResult> PostAsync(string username, PagingParameter pagingParameter)
            => Ok(await _crawlerService.GetPostAsync(username, pagingParameter));
    }
}
