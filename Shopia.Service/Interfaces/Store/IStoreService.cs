using System.Threading;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public interface IStoreService
    {
        Task<bool> SuccessCrawlAsync(string UniqueId, CancellationToken token = default);
    }
}
