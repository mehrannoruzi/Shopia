using Elk.Core;
using Shopia.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Shopia.Delivery.Service
{
    public interface IDeliveryService
    {
        Task<AloPeikAddressInquiry> AddressInquiry(LocationDTO location);
        Task<IResponse<List<DeliveryPrice>>> DeliveryPriceAsync(LocationDTO source, LocationDTO destination);
        Task<AloPeikPriceInquiryResult> PriceInquiry(LocationDTO source, LocationDTO destination, bool cashed, bool hasReturn);
    }
}
