using Elk.Core;
using Shopia.Domain;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public interface IGatewayFactory
    {
        Task<IResponse<(IGatewayService Service, PaymentGateway Gateway)>> GetInsance(int gatewayId);
    }
}