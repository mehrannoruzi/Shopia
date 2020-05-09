using Elk.Core;
using Shopia.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public interface IStoreService
    {
        IResponse<StoreDTO> Find(int id);
        Task<bool> SuccessCrawlAsync(string UniqueId, CancellationToken token = default);
    }
}
