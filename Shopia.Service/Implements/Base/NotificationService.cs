﻿using Elk.Core;
using Shopia.Domain;
using Elk.AspNetCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Shopia.Service
{
    public class NotificationService : INotificationService
    {
        private IConfiguration _configuration { get; }

        public NotificationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<bool> NotifyAsync(NotificationDto notifyDto)
        {
            var requestResult = await HttpRequestTools.PostJsonAsync<IResponse<bool>>(_configuration.GetSection("CustomSettings:NotifierUrl").Value, notifyDto);
            return requestResult.IsSuccessful;
        }
    }
}
