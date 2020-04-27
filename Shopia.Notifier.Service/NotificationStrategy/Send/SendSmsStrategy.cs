using System;
using Elk.Core;
using System.Linq;
using Shopia.Domain;
using System.Threading.Tasks;
using Shopia.Notifier.DataAccess.Dapper;

namespace Shopia.Notifier.Service
{
    public class SendSmsStrategy : ISendStrategy
    {
        private NotifierUnitOfWork _notifierUnitOfWork { get; }

        public SendSmsStrategy(NotifierUnitOfWork notifierUnitOfWork)
        {
            _notifierUnitOfWork = notifierUnitOfWork;
        }


        public Task Send(Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
