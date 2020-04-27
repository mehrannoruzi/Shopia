using Shopia.Domain;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public interface INotificationService
    {
        Task<bool> NotifyAsync(NotificationDto notifyDto);
    }
}
