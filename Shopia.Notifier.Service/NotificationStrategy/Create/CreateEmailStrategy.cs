using Shopia.Domain;
using System.Threading.Tasks;
using Shopia.Notifier.DataAccess.Dapper;

namespace Shopia.Notifier.Service
{
    public class CreateEmailStrategy : ICreateStrategy
    {
        private NotifierUnitOfWork _notifierUnitOfWork { get; }

        public CreateEmailStrategy(NotifierUnitOfWork notifierUnitOfWork)
        {
            _notifierUnitOfWork = notifierUnitOfWork;
        }


        public Task Create(NotificationDto notifyDto)
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
            _notifierUnitOfWork.NotificationRepo.AddAsync(notification);

            return Task.CompletedTask;
        }
    }
}
