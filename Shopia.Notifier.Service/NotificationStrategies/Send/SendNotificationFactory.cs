using Shopia.Domain;
using Shopia.Notifier.DataAccess.Dapper;

namespace Shopia.Notifier.Service
{
    public class SendNotificationFactory
    {
        public static ISendStrategy GetStrategy(NotificationType type, NotifierUnitOfWork notifierUnitOfWork)
        {
            switch (type)
            {
                case NotificationType.Sms:
                    return new SendSmsStrategy(notifierUnitOfWork);

                case NotificationType.TeleBot:
                    return new SendTeleBotStrategy(notifierUnitOfWork);

                case NotificationType.Email:
                    return new SendEmailStrategy(notifierUnitOfWork);

                default:
                    return new SendSmsStrategy(notifierUnitOfWork);
            }
        }
    }
}
