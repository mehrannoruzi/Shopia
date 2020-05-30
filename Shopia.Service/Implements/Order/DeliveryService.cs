﻿using Elk.Core;
using Shopia.Domain;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using Shopia.Service.Resource;
using System.Linq;
using Elk.Http;
using System.Text;
using Shopia.DataAccess.Ef;

namespace Shopia.Service
{
    public class DeliveryService : IDeliveryService
    {
        readonly IStoreService _storeSrv;
        readonly IConfiguration _configuration;
        readonly IGenericRepo<Order> _orderRepo;
        readonly AppUnitOfWork _appUOW;
        public DeliveryService(AppUnitOfWork appUOW, IStoreService storeSrv, IConfiguration configuration)
        {
            _storeSrv = storeSrv;
            _configuration = configuration;
            _appUOW = appUOW;
            _orderRepo = appUOW.OrderRepo;
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

        public async Task<IResponse<int>> Add(int orderId)
        {
            var order = await _appUOW.OrderRepo.FirstOrDefaultAsync(x => x.OrderId == orderId, new List<System.Linq.Expressions.Expression<System.Func<Order, object>>> { x => x.FromAddress, x => x.ToAddress, x => x.User });
            if (order == null) return new Response<int> { Message = ServiceMessage.RecordNotExist };
            UserComment userComment = null;
            try { userComment = order.UserComment.DeSerializeJson<UserComment>(); } catch { }
            using var deliveryPriceHttp = new HttpClient();
            var callDeliveryAPI = await deliveryPriceHttp.PostAsync(_configuration["Delivery:Add"], new StringContent(new DeliveryOrderDTO
            {
                Addresses = new List<DeliveryOrderLocationDTO>
                {
                    new DeliveryOrderLocationDTO
                    {
                        Type = "origin",
                        Lat = order.FromAddress.Latitude,
                        Lng = order.FromAddress.Longitude
                    },
                    new DeliveryOrderLocationDTO
                    {
                        Type = "destination",
                        Lat = order.ToAddress.Latitude,
                        Lng = order.ToAddress.Longitude,
                        Description=order.OrderComment,
                        PersonFullName =userComment?.Reciever,
                        PersonPhone = userComment?.RecieverMobileNumber
                    }
                }
            }.SerializeToJson(), Encoding.UTF8, "application/json"));
            if (!callDeliveryAPI.IsSuccessStatusCode)
            {
                FileLoger.Info($"Add Delivery Api Failed: {callDeliveryAPI.StatusCode}");
                return new Response<int> { IsSuccessful = false };
            }
            var add = (await callDeliveryAPI.Content.ReadAsStringAsync()).DeSerializeJson<Response<OrderResult>>();
            if (!add.IsSuccessful)
            {
                FileLoger.Info($"Add Delivery Api Failed: {add.Message}");
                return new Response<int> { IsSuccessful = false };
            }
            order.DeliveryDetailJson = new
            {
                add.Result.OrderId,
                add.Result.PayAtDestination,
                add.Result.OrderToken

            }.SerializeToJson();
            _appUOW.OrderRepo.Update(order);
            var update = await _appUOW.ElkSaveChangesAsync();
            if (!update.IsSuccessful)
            {
                FileLoger.Info($"Update Ordder DeliveryDetailJson Failed: {update.Message}");
                return new Response<int> { IsSuccessful = false };
            }
            return new Response<int> { IsSuccessful = true };
        }

    }
}
