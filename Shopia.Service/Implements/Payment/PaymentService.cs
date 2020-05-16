using Elk.Core;
using Shopia.Domain;
using Shopia.DataAccess.Ef;
using System.Threading.Tasks;
using Shopia.Service.Resource;

namespace Shopia.Service
{
    public class PaymentService : IPaymentService
    {
        readonly AppUnitOfWork _appUow;
        readonly IGenericRepo<Payment> _paymentRepo;
        public PaymentService(AppUnitOfWork appUOW, IGenericRepo<Payment> paymentRepo)
        {
            _appUow = appUOW;
            _paymentRepo = paymentRepo;
        }

        public async Task<IResponse<Payment>> Add(CreateTransactionRequest model, string transId, int gatewayId)
        {
            var paymentModel = new PaymentAddModel().CopyFrom(model);
            paymentModel.TransactionId = transId;
            paymentModel.GatewayId = gatewayId;
            return await Add(paymentModel);
        }
        public async Task<IResponse<Payment>> Add(PaymentAddModel model)
        {
            var payment = new Payment
            {
                OrderId = model.OrderId,
                PaymentGatewayId = model.GatewayId,
                PaymentStatus = PaymentStatus.Insert,
                Price = model.Amount,
                TransactionId = model.TransactionId
            };
            await _paymentRepo.AddAsync(payment);
            var saveResult = await _appUow.ElkSaveChangesAsync();
            return new Response<Payment>
            {
                IsSuccessful = saveResult.IsSuccessful,
                Message = saveResult.IsSuccessful ? string.Empty : saveResult.Message,
                Result = payment
            };
        }

        public async Task<IResponse<Payment>> FindAsync(string transactionId)
        {
            var payment = await _paymentRepo.FirstOrDefaultAsync(conditions: x => x.TransactionId == transactionId);
            return new Response<Payment>
            {
                IsSuccessful = payment != null,
                Message = payment == null ? ServiceMessage.RecordNotExist : string.Empty,
                Result = payment
            };
        }

        public async Task<IResponse<Payment>> Update(string transactionId, PaymentStatus status)
        {
            var payment = await _paymentRepo.FirstOrDefaultAsync(conditions: x => x.TransactionId == transactionId);
            if (payment == null) return new Response<Payment> { Message = ServiceMessage.RecordNotExist };
            payment.PaymentStatus = status;
            var updateResult = await _appUow.ElkSaveChangesAsync();
            return new Response<Payment>
            {
                IsSuccessful = updateResult.IsSuccessful,
                Message = updateResult.Message,
                Result = payment
            };
        }
    }
}
