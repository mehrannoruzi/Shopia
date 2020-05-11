using Elk.Core;
using System.Linq;
using Shopia.Domain;
using Shopia.DataAccess.Ef;
using System.Threading.Tasks;
using Shopia.Service.Resource;

namespace Shopia.Service
{
    public class OrderService : IOrderService
    {
        readonly AppUnitOfWork _appUow;
        readonly IGenericRepo<Order> _orderRepo;
        readonly IProductService _productSrv;
        readonly IGenericRepo<Product> _productRepo;
        public OrderService(AppUnitOfWork appUOW, IGenericRepo<Order> orderRepo, IGenericRepo<Product> productRepo, IProductService productSrv)
        {
            _appUow = appUOW;
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _productSrv = productSrv;
        }

        public async Task<IResponse<(Order Order, bool IsChanged)>> Add(OrderDTO model)
        {
            var chkResult = await _productSrv.CheckChanges(model.Items);
            var storeId = await _productRepo.FirstOrDefaultAsync(selector: x => x.StoreId, conditions: x => x.ProductId == chkResult.Items.Where(x => x.MaxCount != 0).First().Id);
            var orderDetails = chkResult.Items.Where(x => x.MaxCount != 0).Select(i => new OrderDetail
            {
                ProductId = i.Id,
                Count = i.Count,
                DiscountPrice = i.Count * (i.Price - i.RealPrice),
                Price = i.Price,
                TotalPrice = i.RealPrice * i.Count,
                DiscountPercent = i.Discount
            }).ToList();
            var order = new Order
            {
                StoreId = storeId,
                TotalPrice = orderDetails.Sum(x => x.TotalPrice),
                UserId = model.UserToken,
                DiscountPrice = orderDetails.Sum(x => x.DiscountPrice),
                OrderStatus = OrderStatus.InProcessing,
                DeliveryType = (DeliveryType)model.DeliveryId,
                UserComment = model.Description,
                OrderDetails = orderDetails
            };
            await _orderRepo.AddAsync(order);
            var addOrder = await _appUow.ElkSaveChangesAsync();
            if (!addOrder.IsSuccessful)
                return new Response<(Order, bool)> { Message = addOrder.Message };
            return new Response<(Order, bool)>
            {
                IsSuccessful = true,
                Result = (order, chkResult.Changed)
            };
        }

        public async Task<IResponse<Order>> Update(int orderId, OrderStatus status)
        {
            var order = await _orderRepo.FindAsync(orderId);
            if (order == null) return new Response<Order> { Message = ServiceMessage.RecordNotExist };
            order.OrderStatus = status;
            var update = await _appUow.ElkSaveChangesAsync();
            return new Response<Order>
            {
                IsSuccessful = update.IsSuccessful,
                Result = order,
                Message = update.Message

            };
        }
    }
}
