﻿using Elk.Core;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Shopia.Domain
{
    public interface INotificationRepo : IScopedInjection
    {
        Task<bool> AddAsync(Notification model);
        Task<bool> UpdateAsync(UpdateNotificationDto model);
        Task<Notification> FindAsync(int notificationId);
        Task<IEnumerable<Notification>> GetAsync(NotificationStatus status, PagingParameter pagingParameter);
        Task<IEnumerable<Notification>> GetUnProccessAsync();
    }
}
