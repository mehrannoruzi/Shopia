using Elk.Core;
using Shopia.Domain;
using System;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public interface IOrderService
    {
        Task<IResponse<(Order Order, bool IsChanged)>> Add(OrderDTO model);
        Task<IResponse<Order>> AddTempBasket(TempOrderDTO model);
        Task<IResponse<string>> Verify(Payment payment, object[] args);
        Task<IResponse<Order>> AddAsync(Order model);
        Task<bool> CheckOwner(Guid userId, int orderId);
        Task<IResponse<Order>> UpdateAsync(Order model);
        Task<IResponse<bool>> DeleteAsync(int OrderId);
        Task<IResponse<Order>> FindAsync(int OrderId);
        Task<IResponse<Order>> GetDetails(int OrderId);
        PagingListDetails<Order> Get(OrderSearchFilter filter);
        Task<IResponse<Order>> UpdateStatusAsync(int id, OrderStatus status, bool check = true);
    }
}