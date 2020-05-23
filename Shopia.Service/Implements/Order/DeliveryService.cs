using Elk.Core;
using Shopia.Domain;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using Shopia.Service.Resource;
using System.Linq;
using Elk.Http;
using System.Text;

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

        private async Task<IResponse<List<PriceInquiryResult>>> GetTypes(int storeId, LocationDTO location)
        {
            var getSource = await _storeSrv.GetLocationAsync(storeId);
            if (!getSource.IsSuccessful) return new Response<List<PriceInquiryResult>> { Message = ServiceMessage.RecordNotExist };
            using var deliveryPriceHttp = new HttpClient();
            var callDeliveryAPI = await deliveryPriceHttp.PostAsync(_configuration["Delivery:Price"], new StringContent(new
            {
                Source = getSource.Result,
                Destination = location
            }.SerializeToJson(), Encoding.UTF8, "application/json"));
            if (!callDeliveryAPI.IsSuccessStatusCode) return new Response<List<PriceInquiryResult>> { IsSuccessful = false, Message = ServiceMessage.DeliveryAPIFailed };
            var getDeliveryCost = (await callDeliveryAPI.Content.ReadAsStringAsync()).DeSerializeJson<Response<List<PriceInquiryResult>>>();
            return getDeliveryCost;
        }

        public async Task<IResponse<GetDeliveryTypesDTO>> GetDeliveryTypes(int storeId, LocationDTO location)
        {
            var getTypes = await GetTypes(storeId, location);
            if (!getTypes.IsSuccessful) return new Response<GetDeliveryTypesDTO> { Message = getTypes.Message };
            return new Response<GetDeliveryTypesDTO>
            {
                IsSuccessful = true,
                Result = new GetDeliveryTypesDTO
                {
                    PlaceName = getTypes.Result[0].Addresses.First(x => x.Type == "origin").Address.Substring(0, 20),
                    Items = getTypes.Result.Select(x => new DeliveryDto
                    {
                        Id = x.DeliveryProviderId,
                        Name = x.DeliveryType_Fa,
                        Cost = x.Price
                    }).ToList()
                }
            };
        }

        public async Task<IResponse<int>> GetDeliveryCost(int deliveryId, int storeId, LocationDTO location)
        {
            var getTypes = await GetTypes(storeId, location);
            var type = getTypes.Result.FirstOrDefault(x => x.DeliveryProviderId == deliveryId);
            if (type == null) return new Response<int> { Message = ServiceMessage.InvalidDeliveryType };
            return new Response<int> { IsSuccessful = true, Result = type.Price };
        }

    }
}
