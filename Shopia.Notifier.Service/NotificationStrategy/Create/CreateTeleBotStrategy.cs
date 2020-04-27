using Shopia.Domain;
using System.Threading.Tasks;
using Shopia.Notifier.DataAccess.Dapper;

namespace Shopia.Notifier.Service
{
    public class CreateTeleBotStrategy : ICreateStrategy
    {
        private NotifierUnitOfWork _notifierUnitOfWork { get; }

        public CreateTeleBotStrategy(NotifierUnitOfWork notifierUnitOfWork)
        {
            _notifierUnitOfWork = notifierUnitOfWork;
        }


        public Task Create(NotificationDto notifyDto)
        {
            var notification = new Notification
            {
                TryCount = 0,
                Type = NotificationType.TeleBot,
                Status = NotificationStatus.Insert,

                ExtraData = notifyDto.UserId.ToString(),
                Content = notifyDto.Content,
                FullName = notifyDto.FullName,
                Receiver = notifyDto.TelegramChatId
            };
            _notifierUnitOfWork.NotificationRepo.AddAsync(notification);

            return Task.CompletedTask;
        }
    }
}
