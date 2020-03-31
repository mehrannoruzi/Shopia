using Elk.Core;
using System.Threading.Tasks;

namespace Shopia.Domain
{
    public interface IUserRepo : IGenericRepo<User>, IScopedInjection
    {
        Task<User> FindByMobileNumber(long mobileNumber);
    }
}
