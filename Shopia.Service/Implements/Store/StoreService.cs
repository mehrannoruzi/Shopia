﻿using Elk.Core;
using System.Linq;
using Shopia.Domain;
using System.Threading;
using Shopia.DataAccess.Ef;
using System.Threading.Tasks;
using Shopia.Service.Resource;
using Elk.EntityFrameworkCore;

namespace Shopia.Service
{
    public class StoreService : IStoreService
    {
        readonly AppUnitOfWork _appUow;
        readonly IGenericRepo<Domain.Store> _storeRepo;
        public StoreService(AppUnitOfWork appUOW)
        {
            _appUow = appUOW;
           _storeRepo = appUOW.StoreRepo;
        }

        public async Task<IResponse<StoreDTO>> FindAsync(int id)
        {
            var store = await _storeRepo.FindAsync(id);
            if (store == null) return new Response<StoreDTO>
            {
                IsSuccessful = false,
                Message = ServiceMessage.RecordNotExist
            };

            return new Response<StoreDTO>
            {
                IsSuccessful = true,
                Result = new StoreDTO
                {
                    Name = store.FullName,
                    LogoUrl = store.ProfilePictureUrl
                }
            };
        }

        public async Task<bool> SuccessCrawlAsync(string UniqueId, CancellationToken token = default)
        {
            //await _appUow.NotificationRepo.AddAsync(model, token);
            var saveResult = await _appUow.ElkSaveChangesAsync(token);
            return saveResult.IsSuccessful;
        }


    }
}
