using System;
using Elk.Core;
using System.Linq;
using System.Collections.Generic;
using Elk.Http;
using System.Text;
using Shopia.Domain;
using Shopia.InfraStructure;
using System.Threading.Tasks;

namespace Shopia.Delivery.Service
{
    public static class AloPeikProvider
    {
        public static async Task<AloPeikUser> Authenticate()
        {
            var authResult = await HttpRequestTools.GetAsync<AloPeikResult<AloPeikUser>>(GlobalVariables.DeliveryProviders.AloPeik.Url, Encoding.UTF8);
            if (authResult.Status == "success") return authResult.Object;

            return null;
        }

        public static async Task<AloPeikAddressInquiry> AddressInquiry(LocationDTO location)
        {
            var authResult = await HttpRequestTools.GetAsync<AloPeikResult<AloPeikAddressInquiry>>(
                $"{GlobalVariables.DeliveryProviders.AloPeik.Url}/locations?latlng={location.Lat},{location.Lng}", Encoding.UTF8);
            if (authResult.Status == "success") return authResult.Object;

            return null;
        }

        public static async Task<AloPeikPriceInquiryResult> PriceInquiry(LocationDTO originLocation, LocationDTO destinationLocation, bool cashed, bool hasReturn)
        {
            var authResult = await HttpRequestTools.GetAsync<AloPeikResult<AloPeikPriceInquiryResult>>(
                $"{GlobalVariables.DeliveryProviders.AloPeik.Url}/orders/price/calc",
                new AloPeikPriceInquiry
                {
                    Addresses = new List<AloPeikAddress> {
                        new AloPeikAddress{ Type = AloPeikAddressType.Origin.ToString(), Lat =  originLocation.Lat, Lng = originLocation.Lng},
                        new AloPeikAddress{ Type = AloPeikAddressType.Destination.ToString(), Lat =  destinationLocation.Lat, Lng = destinationLocation.Lng}
                    },
                    Transport_Type = AloPeikTransportType.motor_taxi.ToString(),
                    Cashed = cashed,
                    Has_Return = hasReturn
                },
                typeof(AloPeikPriceInquiry),
                Encoding.UTF8);
            if (authResult.Status == "success") return authResult.Object;

            return null;
        }
    }
}
