using Elk.Core;
using Shopia.Domain;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using Shopia.Service.Resource;
using System.Linq;

namespace Shopia.Service
{
    public class DeliveryService : IDeliveryService
    {
        readonly IStoreService _storeSrv;
        readonly IConfiguration _configuration;
        public DeliveryService(IStoreService storeSrv, IConfiguration configuration)
        {
            _storeSrv = storeSrv;
            _configuration = configuration;
        }

        private async Task<Response<List<PriceInquiryResult>>> GetTypes(int storeId, LocationDTO location)
        {
            var source = await _storeSrv.GetLocationAsync(storeId);
            using var deliveryPriceHttp = new HttpClient();
            var callDeliveryAPI = await deliveryPriceHttp.PostAsync(_configuration["Delivery:Price"], new StringContent(new
            {
                Source = source,
                Destination = location
            }.SerializeToJson()));
            if (!callDeliveryAPI.IsSuccessStatusCode) return new Response<List<PriceInquiryResult>> { IsSuccessful = false, Message = ServiceMessage.DeliveryAPIFailed };
            var getDeliveryCost = (await callDeliveryAPI.Content.ReadAsStringAsync()).DeSerializeJson<Response<List<PriceInquiryResult>>>();
            return getDeliveryCost;
        }

        public async Task<(string PlaceName, List<DeliveryDto> Types)> GetDeliveryTypes(int storeId, LocationDTO location)
        {
            var getTypes = await GetTypes(storeId, location);
            return (getTypes.Result[0].Addresses.First(x => x.Type == "origin").Address.Substring(0, 20), getTypes.Result.Select(x => new DeliveryDto
            {
                Id = x.DeliveryProviderId,
                Name = x.DeliveryType_Fa,
                Cost = x.Price
            }).ToList());
        }

        public async Task<Response<int>> GetDeliveryCost(int deliveryId, int storeId, LocationDTO location)
        {
            var getTypes = await GetTypes(storeId, location);
            var type = getTypes.Result.FirstOrDefault(x => x.DeliveryProviderId == deliveryId);
            if (type == null) return new Response<int> { Message = ServiceMessage.InvalidDeliveryType };
            return new Response<int> { IsSuccessful = true, Result = type.Price };
        }

    }
}
