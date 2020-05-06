using Elk.Core;
using System.Linq;
using Shopia.Domain;
using System.Threading;
using Shopia.DataAccess.Ef;
using System.Threading.Tasks;
using Shopia.Service.Resource;

namespace Shopia.Service
{
    public class StoreService : IStoreService
    {
        readonly AppUnitOfWork _appUow;
        readonly IStoreRepo _storeRepo;
        public StoreService(AppUnitOfWork appUOW)
        {
            _appUow = appUOW;
            _storeRepo = appUOW.StoreRepo;
        }

        public IResponse<StoreDTO> Find(int id)
        {
            var store = _storeRepo.Get(selector: x => new StoreDTO
            {
                Name = x.FullName,
                LogoUrl = x.ProfilePictureUrl
            },
            conditions: x => x.StoreId == id && x.IsActive).FirstOrDefault();
            if (store == null) return new Response<StoreDTO>
            {
                IsSuccessful = false,
                Message = ServiceMessage.RecordNotExist
            };

            return new Response<StoreDTO>
            {
                IsSuccessful = true,
                Result = store
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
