using System;
using Shopia.Domain;
using Shopia.Service;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Elk.Core;
using Microsoft.Extensions.Configuration;
using Shopia.Store.Api.Resources;
using System.Linq;

namespace Shopia.Store.Api.Controllers
{
    public class OrderController : Controller
    {
        readonly IUserService _userService;
        readonly IOrderService _orderService;
        readonly IPaymentService _paymentService;
        readonly IGatewayService _gatewayService;
        readonly IConfiguration _configuration;
        public OrderController(IUserService userService, IOrderService orderService, IPaymentService paymentService, IGatewayService gatewayService, IConfiguration configuration)
        {
            _userService = userService;
            _orderService = orderService;
            _paymentService = paymentService;
            _gatewayService = gatewayService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> CompleteInfo([FromBody]UserDTO model)
        {
            if (model.Token != null)
            {
                var findUser = await _userService.FindAsync(model.Token ?? Guid.Empty);
                if (findUser.IsSuccessful)
                    return Json(new
                    {
                        IsSuccessful = true,
                        Result = findUser.Result.UserId
                    });
            }

            var prevUser = await _userService.FindByMobileNumber(model.MobileNumber);
            if (prevUser.IsSuccessful) return Json(new
            {
                IsSuccessful = true,
                Result = prevUser.Result.UserId
            });
            var addUser = await _userService.AddAsync(new User
            {
                MobileNumber = model.MobileNumber,
                FullName = model.Fullname,
                Password = HashGenerator.Hash(model.MobileNumber.ToString())
            });
            return Json(new
            {
                addUser.IsSuccessful,
                addUser.Message,
                Result = addUser.IsSuccessful ? addUser.Result.UserId : Guid.Empty
            });
        }

        [HttpPost, EnableCors]
        public async Task<IActionResult> Submit([FromBody]OrderDTO order)
        {
            var findUser = await _userService.FindAsync(order.UserToken);
            if (!findUser.IsSuccessful)
                return Json(new Response<string> { Message = Strings.InvalidToken });
            var addOrder = await _orderService.Add(order);
            var transModel = new CreateTransactionRequest
            {
                OrderId = addOrder.Result.Order.OrderId,
                Amount = addOrder.Result.Order.TotalPrice,
                MobileNumber = findUser.Result.MobileNumber.ToString(),
                ApiKey = _configuration["HillaPay:ApiKey"],
                CallbackUrl = _configuration["HillaPay:CallBackUrl"],
                Url = _configuration["HillaPay:TransactionUrl"]
            };
            var createTrans = await _gatewayService.CreateTrasaction(transModel, null);
            if (!createTrans.IsSuccessful)
                return Json(new Response<Order> { Message = createTrans.Message });
            var paymentModel = new PaymentAddModel().CopyFrom(transModel);
            paymentModel.TransactionId = createTrans.Result.TransactionId;
            var addPayment = await _paymentService.Add(paymentModel);
            if (!addPayment.IsSuccessful)
                return Json(new Response<Order> { Message = addPayment.Message });
            return Json(new
            {
                IsSuccessful = true,
                Result = new
                {
                    Id = createTrans.Result.TransactionId,
                    Url = createTrans.Result.GatewayUrl,
                    BasketChanged = addOrder.Result.IsChanged,
                    ChangedProducts = addOrder.Result.Order.OrderDetails.Select(x => new ProductDTO
                    {
                        Id = x.ProductId,
                        Discount = x.DiscountPercent ?? 0,
                        Price = x.Price,
                        MaxCount = x.Count
                    })
                }
            });
        }

    }
}