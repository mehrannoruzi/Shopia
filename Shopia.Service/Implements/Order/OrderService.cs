using Elk.Core;
using System.Linq;
using Shopia.Domain;
using Shopia.DataAccess.Ef;
using System.Threading.Tasks;
using Shopia.Service.Resource;
using System.Linq.Expressions;
using System;

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
                Price = i.Price,
                TotalPrice = i.RealPrice * i.Count,
                DiscountPrice = i.DiscountPrice,
                DiscountPercent = i.Discount
            }).ToList();
            var order = new Order
            {
                StoreId = store.StoreId,
                TotalPrice = orderDetails.Sum(x => x.Price * x.Count),
                TotalPriceAfterDiscount = orderDetails.Sum(x => x.TotalPrice) + getDeliveryCost.Result,
                UserId = model.UserToken,
                DiscountPrice = orderDetails.Sum(x => x.DiscountPrice),
                OrderStatus = OrderStatus.WaitForPayment,
                DeliveryProviderId = model.DeliveryId,
                OrderComment = model.Description,
                UserComment = new UserComment { Reciever = model.Reciever, RecieverMobileNumber = model.RecieverMobileNumber }.SerializeToJson(),
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
            order.OrderStatus = OrderStatus.InProcessing;
            payment.PaymentStatus = PaymentStatus.Success;
            var update = await _appUow.ElkSaveChangesAsync();

            return new Response<string>
            {
                IsSuccessful = update.IsSuccessful,
                Result = payment.TransactionId,
                Message = update.Message

            };
        }

        public async Task<IResponse<Order>> AddAsync(Order model)
        {
            await _orderRepo.AddAsync(model);

            var saveResult = await _appUow.ElkSaveChangesAsync();
            return new Response<Order> { Result = model, IsSuccessful = saveResult.IsSuccessful, Message = saveResult.Message };
        }

        public async Task<IResponse<Order>> UpdateAsync(Order model)
        {
            var findedOrder = await _orderRepo.FindAsync(model.OrderId);
            if (findedOrder == null) return new Response<Order> { Message = ServiceMessage.RecordNotExist };
            //TODO:Set Update Fileds
            var saveResult = _appUow.ElkSaveChangesAsync();
            return new Response<Order> { Result = findedOrder, IsSuccessful = saveResult.Result.IsSuccessful, Message = saveResult.Result.Message };
        }

        public async Task<bool> CheckOwner(Guid userId, int orderId) => await _orderRepo.AnyAsync(x => x.OrderId == orderId && x.Store.UserId == userId);

        public async Task<IResponse<Order>> UpdateStatusAsync(int id, OrderStatus status)
        {
            var order = await _orderRepo.FindAsync(id);
            if (order == null) return new Response<Order> { Message = ServiceMessage.RecordNotExist };
            if (order.OrderStatus == OrderStatus.Successed || (order.OrderStatus == OrderStatus.WaitForPayment && order.OrderStatus != status))
                return new Response<Order> { Message = ServiceMessage.NotAllowedOperation };
            order.OrderStatus = status;
            _orderRepo.Update(order);
            var saveResult = await _appUow.ElkSaveChangesAsync();
            return new Response<Order> { IsSuccessful = saveResult.IsSuccessful, Message = saveResult.Message };

        }

        public async Task<IResponse<bool>> DeleteAsync(int OrderId)
        {
            _appUow.OrderRepo.Delete(new Order { OrderId = OrderId });
            var saveResult = await _appUow.ElkSaveChangesAsync();
            return new Response<bool>
            {
                Message = saveResult.Message,
                Result = saveResult.IsSuccessful,
                IsSuccessful = saveResult.IsSuccessful,
            };
        }

        public async Task<IResponse<Order>> FindAsync(int OrderId)
        {
            var order = await _appUow.OrderRepo.FirstOrDefaultAsync(x => x.OrderId == OrderId, new System.Collections.Generic.List<Expression<Func<Order, object>>>
            {
                x=>x.Store,
                x=>x.User,
                x=>x.ToAddress,
            });
            if (order == null) return new Response<Order> { Message = ServiceMessage.RecordNotExist };
            order.OrderDetails = _appUow.OrderDetailRepo.Get(x => x.OrderId == OrderId, o => o.OrderByDescending(x => x.OrderDetailId), new System.Collections.Generic.List<Expression<Func<OrderDetail, object>>>
            {
                x=>x.Product
            });
            return new Response<Order> { Result = order, IsSuccessful = true };
        }

        public PagingListDetails<Order> Get(OrderSearchFilter filter)
        {
            Expression<Func<Order, bool>> conditions = x => true;
            if (filter != null)
            {
                if (filter.UserId != null) conditions = x => x.UserId == filter.UserId;
                if (filter.StoreId != null) conditions = x => x.StoreId == filter.StoreId;
                if (!string.IsNullOrWhiteSpace(filter.FromDateSh))
                {
                    var dt = PersianDateTime.Parse(filter.FromDateSh).ToDateTime();
                    conditions = x => x.InsertDateMi >= dt;
                }
                if (!string.IsNullOrWhiteSpace(filter.ToDateSh))
                {
                    var dt = PersianDateTime.Parse(filter.ToDateSh).ToDateTime();
                    conditions = x => x.InsertDateMi <= dt;
                }
                if (!string.IsNullOrWhiteSpace(filter.TransactionId))
                    conditions = x => x.Payments.Any(p => p.TransactionId == filter.TransactionId);
                if (filter.OrderStatus != null)
                    conditions = x => x.OrderStatus == filter.OrderStatus;
            }

            return _orderRepo.Get(conditions, filter, x => x.OrderByDescending(i => i.OrderId), new System.Collections.Generic.List<Expression<Func<Order, object>>>
            {
                x=>x.Store,
                x=>x.ToAddress,
                x=>x.User
            });
        }
    }
}
