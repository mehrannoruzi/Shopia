using Elk.Core;
using Shopia.Domain;
using System.Threading;
using Shopia.DataAccess.Ef;
using System.Threading.Tasks;
using Shopia.Service.Resource;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using System.IO;

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
        public async Task<IResponse<Store>> FindAsync(int id)
        {
            var store = await _storeRepo.FindAsync(id);
            if (store == null) return new Response<Store> { Message = ServiceMessage.RecordNotExist };

            return new Response<Store> { Result = store, IsSuccessful = true };
        }
        public PagingListDetails<Store> Get(StoreSearchFilter filter)
        {
            Expression<Func<Store, bool>> conditions = x => !x.IsDeleted;
            if (filter != null)
            {
                if (filter.UserId != null)
                    conditions = conditions.And(x => x.UserId == filter.UserId);
                if (!string.IsNullOrWhiteSpace(filter.Name))
                    conditions = conditions.And(x => x.FullName.Contains(filter.Name));
            }

            return _storeRepo.Get(conditions, filter, x => x.OrderByDescending(i => i.StoreId), new List<Expression<Func<Store, object>>> {
                x=>x.User
            });
        }

        public async Task<IResponse<bool>> DeleteAsync(int id)
        {
            var store = await _storeRepo.FindAsync(id);
            if (store == null) return new Response<bool> { Message = ServiceMessage.RecordNotExist };
            _storeRepo.Delete(store);
            var saveResult = await _appUow.ElkSaveChangesAsync();
            return new Response<bool>
            {
                Message = saveResult.Message,
                Result = saveResult.IsSuccessful,
                IsSuccessful = saveResult.IsSuccessful,
            };
        }

        public async Task<bool> SuccessCrawlAsync(string UniqueId, CancellationToken token = default)
        {
            //await _appUow.NotificationRepo.AddAsync(model, token);
            var saveResult = await _appUow.ElkSaveChangesAsync(token);
            return saveResult.IsSuccessful;
        }

        public async Task<IResponse<Domain.Store>> SignUp(StoreSignUpModel model)
        {
            using var tb = _appUow.Database.BeginTransaction();
            var mobileNumber = long.Parse(model.MobileNumber);
            var user = await _appUow.UserRepo.FirstOrDefaultAsync(conditions: x => x.MobileNumber == mobileNumber, null);
            var store = await _storeRepo.FirstOrDefaultAsync(conditions: x => x.Username == model.Username, null);
            if (store != null) return new Response<Domain.Store> { Message = ServiceMessage.DuplicateRecord };
            var cdt = DateTime.Now;
            store = new Domain.Store
            {
                Username = model.Username,
                //FolowerCount = crawl.FolowerCount,
                //FolowingCount = crawl.FolowingCount,
                StoreType = StoreType.Instagram,
                StoreStatus = StoreStatus.Register,
                ProfilePictureUrl = null,//crawl.ProfilePictureUrl,
                LastCrawlTime = null,//crawl.LastCrawlDate,
                FullName = model.StoreName,//crawl.FullName,
                IsActive = true,
                User = user ?? new User
                {
                    FullName = model.FullName,
                    IsActive = true,
                    MobileNumber = mobileNumber,
                    LastLoginDateMi = cdt,
                    LastLoginDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date),
                    InsertDateMi = cdt,
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
        => _storeRepo.Get(x => !x.IsDeleted && x.UserId == userId, o => o.OrderByDescending(x => x.StoreId), null);

        public IDictionary<object, object> Search(string searchParameter, Guid? userId, int take = 10)
            => _storeRepo.Get(conditions: x => !x.IsDeleted && x.FullName.Contains(searchParameter) && userId == null ? true : x.UserId == userId,
                new PagingParameter
                {
                    PageNumber = 1,
                    PageSize = 6
                },
                o => o.OrderByDescending(x => x.StoreId))
            .Items
            .ToDictionary(k => (object)k.StoreId, v => (object)v.FullName);

        public async Task<IResponse<Store>> UpdateAsync(StoreUpdateModel model)
        {
            var store = await _appUow.StoreRepo.FindAsync(model.StoreId);
            if (store == null) return new Response<Store> { Message = ServiceMessage.RecordNotExist };
            if (store.AddressId != null)
            {
                var addr = await _appUow.AddressRepo.FindAsync(store.AddressId);
                if (addr == null)
                {
                    await _appUow.AddressRepo.AddAsync(new Address
                    {
                        UserId = store.UserId,
                        Latitude = model.Address.Latitude,
                        Longitude = model.Address.Longitude,
                        AddressDetails = model.Address.AddressDetails
                    });
                    var addAddress = await _appUow.ElkSaveChangesAsync();
                    if (addAddress.IsSuccessful) store.AddressId = addr.AddressId;
                    else return new Response<Store> { Message = addAddress.Message };
                }
                else
                {
                    addr.Latitude = model.Address.Latitude;
                    addr.Longitude = model.Address.Longitude;
                    addr.AddressDetails = model.Address.AddressDetails;
                    _appUow.AddressRepo.Update(addr);
                }
            }
            store.FullName = model.FullName;
            store.Username = model.Username;
            if (model.Logo != null)
            {
                var dir = $"/Files/{model.StoreId}";
                if (!FileOperation.CreateDirectory(model.Root + dir))
                    return new Response<Store> { Message = ServiceMessage.SaveFileFailed };
                var relativePath = $"{dir}/logo_{Guid.NewGuid().ToString().Replace("-", "_")}{Path.GetExtension(model.Logo.FileName)}";
                using (var stream = File.Create($"{model.Root}{relativePath.Replace("/", "\\")}"))
                    await model.Logo.CopyToAsync(stream);
                store.ProfilePictureUrl = $"{model.BaseDomain}{relativePath}";
            }
            _storeRepo.Update(store);
            var saveResult = await _appUow.ElkSaveChangesAsync();
            return new Response<Store> { Result = store, IsSuccessful = saveResult.IsSuccessful, Message = saveResult.Message };
        }

        public async Task<IResponse<Store>> UpdateAsync(StoreAdminUpdateModel model)
        {
            var store = await _appUow.StoreRepo.FindAsync(model.StoreId);
            if (store == null) return new Response<Store> { Message = ServiceMessage.RecordNotExist };
            if (store.AddressId != null)
            {
                var addr = await _appUow.AddressRepo.FindAsync(store.AddressId);
                if (addr == null)
                {
                    await _appUow.AddressRepo.AddAsync(new Address
                    {
                        UserId = store.UserId,
                        Latitude = model.Address.Latitude,
                        Longitude = model.Address.Longitude,
                        AddressDetails = model.Address.AddressDetails
                    });
                    var addAddress = await _appUow.ElkSaveChangesAsync();
                    if (addAddress.IsSuccessful) store.AddressId = addr.AddressId;
                    else return new Response<Store> { Message = addAddress.Message };
                }
                else
                {
                    addr.Latitude = model.Address.Latitude;
                    addr.Longitude = model.Address.Longitude;
                    addr.AddressDetails = model.Address.AddressDetails;
                    _appUow.AddressRepo.Update(addr);
                }
            }
            store.FullName = model.FullName;
            store.Username = model.Username;
            store.IsActive = model.IsActive;
            store.ShopiaUrl = model.ShopiaUrl;
            store.FolowerCount = model.FolowerCount;
            store.FolowingCount = model.FolowingCount;
            if (model.Logo != null)
            {
                var dir = $"/Files/{model.StoreId}";
                if (!FileOperation.CreateDirectory(model.Root + dir))
                    return new Response<Store> { Message = ServiceMessage.SaveFileFailed };
                var relativePath = $"{dir}/logo_{Guid.NewGuid().ToString().Replace("-", "_")}{Path.GetExtension(model.Logo.FileName)}";
                using (var stream = File.Create($"{model.Root}{relativePath.Replace("/", "\\")}"))
                    await model.Logo.CopyToAsync(stream);
                store.ProfilePictureUrl = $"{model.BaseDomain}{relativePath}";
            }
            _storeRepo.Update(store);
            var saveResult = await _appUow.ElkSaveChangesAsync();
            return new Response<Store> { Result = store, IsSuccessful = saveResult.IsSuccessful, Message = saveResult.Message };
        }

        public async Task<IResponse<bool>> DeleteFile(string baseDomain, string root, int id)
        {
            var store = await _appUow.StoreRepo.FindAsync(id);
            if (store == null) return new Response<bool> { Message = ServiceMessage.RecordNotExist };
            var url = store.ProfilePictureUrl;
            store.ProfilePictureUrl = null;
            _storeRepo.Update(store);
            var update = await _appUow.ElkSaveChangesAsync();
            if (!update.IsSuccessful) return new Response<bool> { Message = update.Message };
            try
            {
                if (url.StartsWith(baseDomain) && File.Exists(root + url.Replace(baseDomain, "")))
                    File.Delete(root + url.Replace(baseDomain, ""));
                return new Response<bool> { IsSuccessful = true };
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                return new Response<bool> { Message = ServiceMessage.Error };
            }
        }

        public async Task<bool> CheckOwner(int storeId, Guid userId) => await _storeRepo.AnyAsync(x => x.StoreId == storeId && x.UserId == userId);
    }
}
