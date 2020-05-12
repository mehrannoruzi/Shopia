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
        readonly IPaymentService _paymentSrv;
        readonly IGatewayService _gatewaySrv;
        readonly IGenericRepo<Product> _productRepo;
        public OrderService(AppUnitOfWork appUOW, IGenericRepo<Order> orderRepo, IGenericRepo<Product> productRepo, IPaymentService paymentSrv, IGatewayService gatewaySrv, IProductService productSrv)
        {
            _appUow = appUOW;
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _productSrv = productSrv;
            _paymentSrv = paymentSrv;
            _gatewaySrv = gatewaySrv;
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
                //DeliveryType = (DeliveryType)model.DeliveryId,
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
