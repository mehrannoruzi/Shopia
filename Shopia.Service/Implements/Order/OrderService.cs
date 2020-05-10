using Elk.Core;
using System.Linq;
using Shopia.Domain;
using Shopia.DataAccess.Ef;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public class OrderService
    {
        readonly AppUnitOfWork _appUow;
        readonly IOrderRepo _orderRepo;
        readonly IProductService _productSrv;
        public OrderService(AppUnitOfWork appUOW, IProductService productSrv)
        {
            _appUow = appUOW;
            _orderRepo = appUOW.OrderRepo;
            _productSrv = productSrv;
        }

        public async Task<IResponse<Order>> Add(OrderDTO model)
        {
            var chkResult = await _productSrv.CheckChanges(model.Items);
            var storeId = await _appUow.ProductRepo.FirstOrDefaultAsync(selector: x => x.StoreId, conditions: x => x.ProductId == chkResult.Items.Where(x => x.MaxCount != 0).First().Id);
            var orderDetails = chkResult.Items.Where(x => x.MaxCount != 0).Select(i => new OrderDetail
            {
                ProductId = i.Id,
                Count = i.Count,
                DiscountPrice = i.Count * (i.Price - i.RealPrice),
                Price = i.Price,
                TotalPrice = i.RealPrice * i.Count
            }).ToList();
            await _orderRepo.AddAsync(new Order
            {
                StoreId = storeId,
                DiscountPrice = orderDetails.Sum(x => x.DiscountPrice),
                OrderStatus = OrderStatus.InProcessing,
                DeliveryType = (DeliveryType)model.DeliveryId,
                OrderDetails = orderDetails
            });
            var addOrder = await _appUow.ElkSaveChangesAsync();
            return null;
        }
    }
}
