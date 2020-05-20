using Elk.Core;
using Shopia.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public interface IDeliveryService
    {
        Task<Response<int>> GetDeliveryCost(int deliveryId, int storeId, LocationDTO location);
        Task<(string PlaceName, List<DeliveryDto> Types)> GetDeliveryTypes(int storeId, LocationDTO location);
    }
}