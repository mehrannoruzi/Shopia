using Shopia.Domain;
using System.Threading.Tasks;

namespace Shopia.Notifier.Service
{
    public class CreateEmailStrategy : ICreateStrategy
    {
        public Task Create(NotificationDto notifyDto, INotificationRepo notificationRepo)
        {
            var notification = new Notification
            {
                TryCount = 0,
                Type = NotificationType.Email,
                Status = NotificationStatus.Insert,

                ExtraData = notifyDto.UserId.ToString(),
                Content = notifyDto.Content,
                FullName = notifyDto.FullName,
                Receiver = notifyDto.Email
            };
            notificationRepo.AddAsync(notification);

            return Task.CompletedTask;
        }
    }
}
