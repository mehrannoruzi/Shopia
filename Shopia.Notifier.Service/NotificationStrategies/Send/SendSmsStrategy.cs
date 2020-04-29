using System;
using Elk.Core;
using System.Linq;
using Shopia.Domain;
using System.Threading.Tasks;
using Shopia.Notifier.DataAccess.Dapper;
using PayamakProvider;

namespace Shopia.Notifier.Service
{
    public class SendSmsStrategy : ISendStrategy
    {
        private NotifierUnitOfWork _notifierUnitOfWork { get; }

        public SendSmsStrategy(NotifierUnitOfWork notifierUnitOfWork)
        {
            _notifierUnitOfWork = notifierUnitOfWork;
        }


        public async Task SendAsync(Notification notification)
        {
            var sendResult = await LinePayamakProvider.SendSmsAsync(notification.Receiver, notification.Content);

            var updateModel = new UpdateNotificationDto
            {
                NotificationId = notification.NotificationId,
                SendDateMi = DateTime.Now,
                SendStatus = sendResult
            };

            if (sendResult.Split(':')[1] == "1")
                updateModel.Status = NotificationStatus.Success;
            else
                updateModel.Status = NotificationStatus.Failed;

            await _notifierUnitOfWork.NotificationRepo.UpdateAsync(updateModel);
        }
    }
}
