using Shopia.Domain;
using System.Threading.Tasks;
using Shopia.Notifier.DataAccess.Dapper;

namespace Shopia.Notifier.Service
{
    public interface ISendStrategy
    {
        Task SendAsync(Notification notification, INotificationRepo notificationRepo);
    }
}
