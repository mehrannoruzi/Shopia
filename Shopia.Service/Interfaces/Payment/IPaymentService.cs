using Elk.Core;
using Shopia.Domain;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public interface IPaymentService
    {
        Task<IResponse<Payment>> Add(CreateTransactionRequest model, string transId, int gatewayId);
        Task<IResponse<Payment>> Add(PaymentAddModel model);
        Task<IResponse<Payment>> FindAsync(string transactionId);
        Task<IResponse<Payment>> Update(string transactionId, PaymentStatus status);
    }
}