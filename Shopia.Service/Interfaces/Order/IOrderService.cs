using Elk.Core;
using Shopia.Domain;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public interface IOrderService
    {
        Task<IResponse<(Order Order, bool IsChanged)>> Add(OrderDTO model);
        Task<IResponse<Order>> Update(int orderId, OrderStatus status);
    }
}