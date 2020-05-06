using Shopia.Domain;
using System.Threading;
using Shopia.DataAccess.Ef;
using System.Threading.Tasks;
using Elk.Core;

namespace Shopia.Service
{
    public class StoreService : IStoreService
    {
        readonly AppUnitOfWork _appUow;
        readonly StoreRepo _storeRepo;
        public StoreService(AppUnitOfWork appUOW)
        {
            _appUow = appUOW;
            _storeRepo = appUOW.StoreRepo;
        }

        public async Task<IResponse<Store>> Find(int id)
        {
            var store = _storeRepo.FindAsync(id);
            store.
        }

        public async Task<bool> SuccessCrawlAsync(string UniqueId, CancellationToken token = default)
        {
            //await _appUow.NotificationRepo.AddAsync(model, token);
            var saveResult = await _appUow.ElkSaveChangesAsync(token);
            return saveResult.IsSuccessful;
        }


    }
}
