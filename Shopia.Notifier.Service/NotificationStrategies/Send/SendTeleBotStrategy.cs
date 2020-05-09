﻿using System;
using Shopia.Domain;
using System.Threading.Tasks;

namespace Shopia.Notifier.Service
{
    public class SendTeleBotStrategy : ISendStrategy
    {
        public async Task SendAsync(Notification notification, INotificationRepo notificationRepo)
        {
            await TelegramBot._client.SendTextMessageAsync(notification.Receiver, notification.Content);

            var updateModel = new UpdateNotificationDto
            {
                NotificationId = notification.NotificationId,
                Status = NotificationStatus.Success,
                SendDateMi = DateTime.Now,
                SendStatus = "Success"
            };
            await notificationRepo.UpdateAsync(updateModel);
        }
    }
}
