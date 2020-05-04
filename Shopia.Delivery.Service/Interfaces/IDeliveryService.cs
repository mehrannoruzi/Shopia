using Elk.Core;
using Shopia.Domain;
using System.Threading.Tasks;

namespace Shopia.Delivery.Service
{
    public interface IDeliveryService
    {
        Task<IResponse<int>> InquiryDeliveryPriceAsync(DeliveryType type);
    }
}
