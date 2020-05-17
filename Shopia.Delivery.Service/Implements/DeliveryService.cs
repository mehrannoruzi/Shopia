using System;
using Elk.Core;
using System.Linq;
using Shopia.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shopia.Delivery.Service.Resource;

namespace Shopia.Delivery.Service
{
    public class DeliveryService : IDeliveryService
    {
        public async Task<AloPeikAddressInquiry> AddressInquiry(LocationDTO location)
            => await AloPeikProvider.AddressInquiry(location);

        public async Task<AloPeikPriceInquiryResult> PriceInquiry(LocationDTO source, LocationDTO destination, bool cashed, bool hasReturn)
            => await AloPeikProvider.PriceInquiry(source, destination, cashed, hasReturn);

        public async Task<IResponse<List<DeliveryPrice>>> DeliveryPriceAsync(LocationDTO source, LocationDTO destination)
        {
            var response = new Response<List<DeliveryPrice>>();
            try
            {
                var InquiryResult = new List<DeliveryPrice>();

                foreach (var item in EnumExtension.GetEnumElements<DeliveryType>())
                {
                    if (item.Name == DeliveryType.Peik.ToString())
                    {
                        InquiryResult.Add(new DeliveryPrice { Price = 20000, Name = DeliveryType.Peik.ToString(), DeliveryProviderId = 1, DeliveryType = DeliveryType.Peik, Address = "زعفرانیه" });
                    }
                    else if (item.Name == DeliveryType.Post.ToString())
                    {
                        InquiryResult.Add(new DeliveryPrice { Price = 7000, Name = DeliveryType.Post.ToString(), DeliveryProviderId = 2, DeliveryType = DeliveryType.Post, Address = "زعفرانیه" });
                    }
                }

                response.IsSuccessful = true;
                response.Result = InquiryResult;
                response.Message = ServiceMessage.Success;
                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);

                response.Message = ServiceMessage.Exception;
                return response;
            }
        }
    }
}
