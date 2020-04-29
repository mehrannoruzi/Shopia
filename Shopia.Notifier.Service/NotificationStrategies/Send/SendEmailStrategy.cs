﻿using System;
using Elk.Core;
using System.Linq;
using Shopia.Domain;
using System.Threading.Tasks;
using Shopia.Notifier.DataAccess.Dapper;

namespace Shopia.Notifier.Service
{
    public class SendEmailStrategy : ISendStrategy
    {
        private NotifierUnitOfWork _notifierUnitOfWork { get; }

        public SendEmailStrategy(NotifierUnitOfWork notifierUnitOfWork)
        {
            _notifierUnitOfWork = notifierUnitOfWork;
        }


        public Task SendAsync(Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
