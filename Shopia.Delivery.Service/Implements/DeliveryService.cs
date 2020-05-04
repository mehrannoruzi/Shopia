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
        public async Task<IResponse<int>> InquiryDeliveryPriceAsync(DeliveryType type)
        {
            var response = new Response<int>();
            try
            {
                switch (type)
                {
                    case DeliveryType.Peik:
                        response.Result = 10000;
                        response.IsSuccessful = true;
                        response.Message = ServiceMessage.Success;
                        break;
                    case DeliveryType.Post:
                        response.Result = 12000;
                        response.IsSuccessful = true;
                        response.Message = ServiceMessage.Success;
                        break;
                }

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
