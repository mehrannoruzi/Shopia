using Elk.Core;
using Shopia.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public interface IStoreService
    {
        Task<IResponse<StoreDTO>> FindAsync(int id);
        Task<bool> SuccessCrawlAsync(string UniqueId, CancellationToken token = default);
    }
}
