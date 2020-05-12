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
        readonly IGatewayService _gatewaySrv;
        readonly IGenericRepo<Product> _productRepo;
        readonly IGenericRepo<Address> _addressRepo;
        public OrderService(AppUnitOfWork appUOW, IGenericRepo<Order> orderRepo, IGenericRepo<Product> productRepo, IGatewayService gatewaySrv, IProductService productSrv, IGenericRepo<Address> addressRepo)
        {
            _appUow = appUOW;
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _productSrv = productSrv;
            _gatewaySrv = gatewaySrv;
            _addressRepo = addressRepo;
        }

        public async Task<IResponse<(Order Order, bool IsChanged)>> Add(OrderDTO model)
        {
            var chkResult = await _productSrv.CheckChanges(model.Items);
            var productId = chkResult.Items.Where(x => x.Count != 0).First().Id;
            var store = await _productRepo.FirstOrDefaultAsync(x => new { x.StoreId, x.Store.AddressId }, x => x.ProductId == productId);
            if (store == null) return new Response<(Order, bool)> { Message = ServiceMessage.RecordNotExist };

            var orderDetails = chkResult.Items.Where(x => x.Count != 0).Select(i => new OrderDetail
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
                StoreId = store.StoreId,
                TotalPrice = orderDetails.Sum(x => x.TotalPrice),
                UserId = model.UserToken,
                DiscountPrice = orderDetails.Sum(x => x.DiscountPrice),
                OrderStatus = OrderStatus.InProcessing,
                DeliveryType = (DeliveryType)model.DeliveryId,
                UserComment = model.Description,
                ToAddressId = model.Address.Id ?? 0,
                ToAddress = model.Address.Id == null ? new Address
                {
                    UserId = model.UserToken,
                    AddressType = AddressType.Home,
                    Latitude = model.Address.Lat,
                    Longitude = model.Address.Lng,
                    AddressDetails = model.Address.Address
                } : null,
                FromAddressId = store.AddressId ?? 0,
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

        public async Task<IResponse<string>> Verify(Payment payment, VerifyRequest request, object[] args)
        {
            var verify = await _gatewaySrv.VerifyTransaction(request, args);
            if (!verify.IsSuccessful) return new Response<string> { IsSuccessful = false, Result = payment.TransactionId };
            var order = await _orderRepo.FindAsync(payment.OrderId);
            if (order == null) return new Response<string> { Message = ServiceMessage.RecordNotExist };
            order.OrderStatus = OrderStatus.WaitForDelivery;
            payment.PaymentStatus = PaymentStatus.Success;
            var update = await _appUow.ElkSaveChangesAsync();

            return new Response<string>
            {
                IsSuccessful = update.IsSuccessful,
                Result = payment.TransactionId,
                Message = update.Message

            };
        }
    }
}
