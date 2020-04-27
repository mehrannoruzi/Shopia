using Elk.Core;
using Shopia.Domain;
using System.Threading.Tasks;

namespace Shopia.Notifier.Service
{
    public interface INotificationService
    {
        Task<IResponse<bool>> AddAsync(NotificationDto notifyDto);
        
        Task SendAsync();
    }
}
