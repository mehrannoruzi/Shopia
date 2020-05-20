using System;
using Elk.Core;
using Shopia.Domain;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Shopia.Service
{
    public interface IStoreService
    {
        Task<IResponse<LocationDTO>> GetLocationAsync(int id);
        Task<IResponse<StoreDTO>> FindAsDtoAsync(int id);
        Task<bool> SuccessCrawlAsync(string UniqueId, CancellationToken token = default);
        Task<IResponse<Domain.Store>> SignUp(StoreSignUpModel model, CrawledPageDto crawl);
        IEnumerable<Domain.Store> GetAll(Guid userId);

        IDictionary<object, object> Search(string searchParameter, Guid? userId, int take = 10);
    }
}
