using Shopia.Domain;
using System.Threading.Tasks;

namespace Shopia.Notifier.Service
{
    public interface ISendStrategy
    {
        Task Send(Notification notification);
    }
}
