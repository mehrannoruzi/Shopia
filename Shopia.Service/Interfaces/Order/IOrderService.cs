using Elk.Core;
using Shopia.Domain;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public interface IOrderService
    {
        Task<IResponse<(Order Order, bool IsChanged)>> Add(OrderDTO model);
        Task<IResponse<string>> Verify(Payment payment, object[] args);
    }
}