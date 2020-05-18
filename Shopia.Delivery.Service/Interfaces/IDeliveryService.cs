using Elk.Core;
using Shopia.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Shopia.Delivery.Service
{
    public interface IDeliveryService
    {
        Task<IResponse<dynamic>> AddressInquiry(LocationDTO location);
        Task<IResponse<List<PriceInquiryResult>>> PriceInquiry(LocationsDTO priceInquiry, bool cashed, bool hasReturn);
        Task<IResponse<OrderResult>> RegisterPeykOrder(DeliveryOrderDTO deliveryOrderDTO);
        Task<IResponse<OrderResult>> RegisterPostOrder(DeliveryOrderDTO deliveryOrderDTO);
    }
}
