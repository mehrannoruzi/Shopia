using Elk.Core;
using Shopia.Domain;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public interface IGatewayService
    {
        Task<IResponse<CreateTransactionReponse>> CreateTrasaction(CreateTransactionRequest model, object[] args);
        Task<IResponse<string>> VerifyTransaction(VerifyRequest model, object[] args);
    }
}