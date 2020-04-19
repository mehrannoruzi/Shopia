using Shopia.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Shopia.Service    
{
    public interface INotificationService
    {
        Task<bool> AddAsync(Notification model, CancellationToken token = default);
    }
}
