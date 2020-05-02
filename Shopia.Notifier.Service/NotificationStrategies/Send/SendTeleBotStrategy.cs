using System;
using Shopia.Domain;
using System.Threading.Tasks;
using Shopia.Notifier.DataAccess.Dapper;

namespace Shopia.Notifier.Service
{
    public class SendTeleBotStrategy : ISendStrategy
    {
        private NotifierUnitOfWork _notifierUnitOfWork { get; }

        public SendTeleBotStrategy(NotifierUnitOfWork notifierUnitOfWork)
        {
            _notifierUnitOfWork = notifierUnitOfWork;
        }


        public async Task SendAsync(Notification notification)
        {
            await TelegramBot._client.SendTextMessageAsync(notification.Receiver, notification.Content);

            var updateModel = new UpdateNotificationDto
            {
                NotificationId = notification.NotificationId,
                Status = NotificationStatus.Success,
                SendDateMi = DateTime.Now,
                SendStatus = "Success"
            };
            await _notifierUnitOfWork.NotificationRepo.UpdateAsync(updateModel);
        }
    }
}
