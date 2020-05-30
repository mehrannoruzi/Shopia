using Elk.Core;
using Shopia.Domain;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public interface IDeliveryService
    {
        Task<IResponse<int>> GetDeliveryCost(int deliveryId, int storeId, LocationDTO location);
        Task<IResponse<GetDeliveryTypesDTO>> GetDeliveryTypes(int storeId, LocationDTO location);
        Task<IResponse<int>> Add(int orderId);
    }
}