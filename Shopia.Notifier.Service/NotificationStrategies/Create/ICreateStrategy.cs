using Shopia.Domain;
using System.Threading.Tasks;

namespace Shopia.Notifier.Service
{
    public interface ICreateStrategy
    {
        Task Create(NotificationDto notifyDto, INotificationRepo notificationRepo);
    }
}