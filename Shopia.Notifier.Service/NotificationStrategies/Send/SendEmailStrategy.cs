using System;
using Shopia.Domain;
using System.Threading.Tasks;

namespace Shopia.Notifier.Service
{
    public class SendEmailStrategy : ISendStrategy
    {
        public Task SendAsync(Notification notification, INotificationRepo notifierUnitOfWork)
        {
            throw new NotImplementedException();
        }
    }
}
