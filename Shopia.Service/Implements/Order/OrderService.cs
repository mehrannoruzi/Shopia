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
        readonly IGatewayFactory _gatewayFactory;
        readonly IDeliveryService _deliverySrv;
        public OrderService(AppUnitOfWork appUOW, IGatewayFactory gatewayFactory, IProductService productSrv, IDeliveryService deliverySrv)
        {
            _appUow = appUOW;
            _orderRepo = appUOW.OrderRepo;
            _productSrv = productSrv;
            _gatewayFactory = gatewayFactory;
            _deliverySrv = deliverySrv;
        }

        public async Task<IResponse<(Order Order, bool IsChanged)>> Add(OrderDTO model)
        {
            var chkResult = await _productSrv.CheckChanges(model.Items);
            var productId = chkResult.Items.Where(x => x.Count != 0).First().Id;
            var store = await _appUow.ProductRepo.FirstOrDefaultAsync(x => new { x.StoreId, x.Store.AddressId }, x => x.ProductId == productId);
            if (store == null) return new Response<(Order, bool)> { Message = ServiceMessage.RecordNotExist };
            var address = await _appUow.AddressRepo.FindAsync(store.AddressId);
            if (address == null) await _appUow.AddressRepo.FindAsync(store.AddressId);
            var getDeliveryCost = await _deliverySrv.GetDeliveryCost(model.DeliveryId, store.StoreId, new LocationDTO { Lat = model.Address.Lat, Lng = model.Address.Lng });
            if (!getDeliveryCost.IsSuccessful) return new Response<(Order, bool)> { Message = getDeliveryCost.Message };
            var orderDetails = chkResult.Items.Where(x => x.Count != 0).Select(i => new OrderDetail
            {
                ProductId = i.Id,
                Count = i.Count,
                DiscountPrice = i.DiscountPrice,
                Price = i.Price,
                TotalPrice = i.RealPrice * i.Count,
                DiscountPercent = i.Discount
            }).ToList();
            var order = new Order
            {
                StoreId = store.StoreId,
                TotalPrice = orderDetails.Sum(x => x.Price * x.Count),
                TotalPriceAfterDiscount = orderDetails.Sum(x => x.TotalPrice) + getDeliveryCost.Result,
                UserId = model.UserToken,
                DiscountPrice = orderDetails.Sum(x => x.DiscountPrice),
                OrderStatus = OrderStatus.InProcessing,
                DeliveryProviderId = model.DeliveryId,
                OrderComment = model.Description,
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

        public async Task<IResponse<string>> Verify(Payment payment, object[] args)
        {
            var findGateway = await _gatewayFactory.GetInsance(payment.PaymentGatewayId);
            if (!findGateway.IsSuccessful)
                return new Response<string> { Message = ServiceMessage.RecordNotExist };
            var verify = await findGateway.Result.Service.VerifyTransaction(new VerifyRequest
            {
                OrderId = payment.OrderId,
                TransactionId = payment.TransactionId,
                ApiKey = findGateway.Result.Gateway.MerchantId,
                Url = findGateway.Result.Gateway.VerifyUrl
            }, args);
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
