using Elk.Core;
using Shopia.Domain;
using System.Threading;
using Shopia.DataAccess.Ef;
using System.Threading.Tasks;
using Shopia.Service.Resource;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Shopia.Service
{
    public class StoreService : IStoreService
    {
        readonly AppUnitOfWork _appUow;
        readonly AuthUnitOfWork _authUow;
        readonly IGenericRepo<Store> _storeRepo;

        public StoreService(AppUnitOfWork appUOW, AuthUnitOfWork authUow)
        {
            _appUow = appUOW;
            _authUow = authUow;
            _storeRepo = appUOW.StoreRepo;
        }

        public async Task<IResponse<LocationDTO>> GetLocationAsync(int id)
        {
            var addressId = await _storeRepo.FirstOrDefaultAsync(x => x.AddressId, x => x.StoreId == id && x.IsActive, includeProperties: null);//await _storeRepo.FirstOrDefaultAsync(x => x.AddressId, x => x.StoreId == id && x.IsActive, includeProperties: null);
            if (addressId == null) return new Response<LocationDTO> { Message = ServiceMessage.RecordNotExist };
            var address = await _appUow.AddressRepo.FindAsync(addressId);
            if (address == null) return new Response<LocationDTO> { Message = ServiceMessage.RecordNotExist };
            return new Response<LocationDTO>
            {
                IsSuccessful = true,
                Result = new LocationDTO
                {
                    Lat = address.Latitude,
                    Lng = address.Longitude
                }
            };
        }
        public async Task<IResponse<StoreDTO>> FindAsDtoAsync(int id)
        {
            try
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
            catch (Exception e)
            {

                return new Response<StoreDTO> { };
            }

        }

        public async Task<bool> SuccessCrawlAsync(string UniqueId, CancellationToken token = default)
        {
            //await _appUow.NotificationRepo.AddAsync(model, token);
            var saveResult = await _appUow.ElkSaveChangesAsync(token);
            return saveResult.IsSuccessful;
        }

        public async Task<IResponse<Domain.Store>> SignUp(StoreSignUpModel model, CrawledPageDto crawl)
        {
            using var tb = _appUow.Database.BeginTransaction();
            var mobileNumber = long.Parse(model.MobileNumber);
            var user = await _appUow.UserRepo.FirstOrDefaultAsync(conditions: x => x.MobileNumber == mobileNumber, null);
            var store = await _storeRepo.FirstOrDefaultAsync(conditions: x => x.Username == model.Username, null);
            if (store != null) return new Response<Domain.Store> { Message = ServiceMessage.DuplicateRecord };
            store = new Domain.Store
            {
                Username = model.Username,
                FolowerCount = crawl.FolowerCount,
                FolowingCount = crawl.FolowingCount,
                StoreType = StoreType.Instagram,
                StoreStatus = StoreStatus.Register,
                ProfilePictureUrl = crawl.ProfilePictureUrl,
                LastCrawlTime = crawl.LastCrawlDate,
                FullName = crawl.FullName,
                IsActive = true,
                User = user ?? new User
                {
                    FullName = model.FullName,
                    IsActive = true,
                    MobileNumber = mobileNumber,
                    Password = HashGenerator.Hash(mobileNumber.ToString()),
                    NewPassword = HashGenerator.Hash(mobileNumber.ToString()),
                    MustChangePassword = false,
                    UserStatus = UserStatus.AddStore
                }
            };
            await _storeRepo.AddAsync(store);
            var saveStore = await _appUow.ElkSaveChangesAsync();
            if (!saveStore.IsSuccessful)
            {
                tb.Rollback();
                return new Response<Domain.Store> { Message = saveStore.Message };
            }
            if (user == null)
            {
                await _authUow.UserInRoleRepo.AddAsync(new UserInRole
                {
                    UserId = store.UserId,
                    RoleId = model.StoreRoleId ?? 0
                });
                var saveUserInRole = await _authUow.ElkSaveChangesAsync();
                if (!saveUserInRole.IsSuccessful) tb.Rollback();
                else tb.Commit();
                return new Response<Domain.Store>
                {
                    IsSuccessful = saveUserInRole.IsSuccessful,
                    Result = store,
                    Message = saveStore.Message
                };
            }
            tb.Commit();
            return new Response<Domain.Store>
            {
                IsSuccessful = true,
                Result = store
            };

        }

        public IEnumerable<Domain.Store> GetAll(Guid userId)
        => _storeRepo.Get(x => x.UserId == userId, o => o.OrderByDescending(x => x.StoreId), null);

        public IDictionary<object, object> Search(string searchParameter, Guid? userId, int take = 10)
            => _storeRepo.Get(conditions: x => x.FullName.Contains(searchParameter) && userId == null ? true : x.UserId == userId)
            .OrderByDescending(x => x.StoreId)
            .Take(take)
            .ToDictionary(k => (object)k.StoreId, v => (object)v.FullName);

    }
}
