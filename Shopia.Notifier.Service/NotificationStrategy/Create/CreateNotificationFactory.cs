using Shopia.Notifier.DataAccess.Dapper;

namespace Shopia.Notifier.Service
{
    public class CreateNotificationFactory
    {
        public static ICreateStrategy GetStrategy(string NotifyStrategy, NotifierUnitOfWork notifierUnitOfWork)
        {
            switch (NotifyStrategy.ToLower())
            {
                case "sms":
                    return new CreateSmsStrategy(notifierUnitOfWork);
                    
                case "telebot":
                    return new CreateTeleBotStrategy(notifierUnitOfWork);
                    
                case "email":
                    return new CreateEmailStrategy(notifierUnitOfWork);
                    

                default:
                    return new CreateSmsStrategy(notifierUnitOfWork);
            }
        }
    }
}
