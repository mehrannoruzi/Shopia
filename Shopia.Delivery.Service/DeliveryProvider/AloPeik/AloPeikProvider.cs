using System;
using Elk.Core;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elk.Http;
using Shopia.InfraStructure;
using Shopia.Domain;
using System.Text;

namespace Shopia.Delivery.Service
{
    public class AloPeikProvider
    {
        public static async Task<AloPeikUser> Authenticate()
        {
            var authResult = await HttpRequestTools.GetAsync<AloPeikAuthResult>(GlobalVariables.DeliveryProviders.AloPeik.Url, Encoding.UTF8);
            if (authResult.Status == "success") return authResult.Object.User;

            return null;
        }

        public static async Task<AloPeikUser> GetAddressDetails(long lat, long lng)
        {
            var authResult = await HttpRequestTools.GetAsync<AloPeikAuthResult>(
                $"{GlobalVariables.DeliveryProviders.AloPeik.Url}/locations?latlng={lat},{lng}", Encoding.UTF8);
            if (authResult.Status == "success") return authResult.Object.User;

            return null;
        }
    }
}
