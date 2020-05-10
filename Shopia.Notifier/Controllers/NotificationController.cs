using Shopia.Domain;
using System.Threading.Tasks;
using Shopia.Notifier.Service;
using Microsoft.AspNetCore.Mvc;

namespace Shopia.Notifier.Controllers
{
    public class NotificationController : Controller
    {
        public INotificationService _notificationService { get; }

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }


        [HttpGet]
        public IActionResult Index()
            => Ok("WellCome To Shopia.Notifier Api ...");

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] NotificationDto notifyDto)
            => Ok(await _notificationService.AddAsync(notifyDto));

    }
}