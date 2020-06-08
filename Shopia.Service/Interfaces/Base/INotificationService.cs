using Elk.Core;
using Shopia.Domain;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public interface INotificationService
    {
        Task<IResponse<bool>> NotifyAsync(NotificationDto notifyDto);
    }
}
