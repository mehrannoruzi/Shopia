using Shopia.Domain;
using System.Threading.Tasks;

namespace Shopia.Notifier.Service
{
    public class CreateSmsStrategy : ICreateStrategy
    {
        public Task Create(NotificationDto notifyDto, INotificationRepo notificationRepo)
        {
            var notification = new Notification { 
                TryCount = 0,
                Type = NotificationType.Sms,
                Status = NotificationStatus.Insert,

                ExtraData = notifyDto.UserId.ToString(),
                Content = notifyDto.Content,
                FullName = notifyDto.FullName,
                Receiver = notifyDto.MobileNumber.ToString()
            };
            notificationRepo.AddAsync(notification);

            return Task.CompletedTask;
        }
    }
}