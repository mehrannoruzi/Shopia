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
    public class AloPeikProvider
    {
        public static async Task<AloPeikUser> Authenticate()
        {
            var authResult = await HttpRequestTools.GetAsync<AloPeikAuthResult>(GlobalVariables.DeliveryProviders.AloPeik.Url, Encoding.UTF8);
            if (authResult.Status == "success") return authResult.Object.User;

            return null;
        }

        public static async Task<AloPeikUser> GetAddressDetails(LocationDTO location)
        {
            var authResult = await HttpRequestTools.GetAsync<AloPeikAuthResult>(
                $"{GlobalVariables.DeliveryProviders.AloPeik.Url}/locations?latlng={location.Lat},{location.Lng}", Encoding.UTF8);
            if (authResult.Status == "success") return authResult.Object.User;

            return null;
        }
    }
}
