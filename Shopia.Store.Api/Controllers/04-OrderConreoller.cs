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

namespace Shopia.Store.Api.Controllers
{
    public class OrderController : Controller
    {
        readonly IUserService _userService;
        //readonly IUserService _userService;
        readonly IConfiguration _configuration;
        public OrderController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> CompleteInfo([FromBody]UserDTO model)
        {
            User user = null;
            if (model.Token == Guid.Empty)
            {
                var findUser = await _userService.FindAsync(model.Token??Guid.Empty);
                if (!findUser.IsSuccessful)
                    user = findUser.Result;
            }
            if (user == null)
            {
                var addUser = await _userService.AddAsync(new User
                {
                    MobileNumber = model.MobileNumber,
                    FullName = model.Fullname,
                    Password = HashGenerator.Hash(model.MobileNumber.ToString())
                });
                if (!addUser.IsSuccessful)
                    return Json(new
                    {
                        IsSuccessful = false,
                        addUser.Message
                    });
                else return Json(new
                {
                    IsSuccessful = true,
                    Result = addUser.Result.UserId
                });
            }
            return Json(new
            {
                IsSuccessful = true,
                Result = model.Token
            });
        }

        [HttpPost, EnableCors]
        public async Task<IActionResult> Submit([FromBody]OrderDTO order)
        {
            var hillapayUrl = "https://api.hillapay.ir/ipg/v3/send";
            if (order.OrderId == null)
            {
                //TODO:Add TO DB AND Calculate Total Price Again
                //add order
                //add order items

            }
            else
            {
                //TODO:Find Order By OrderId
            }
            var orderId = DateTime.Now.Ticks;
            var temp = Constant.Instance();
            temp.OrderId = orderId;

            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Add("api-key", _configuration["HillPay:ApiKey"]);
                var model = new
                {
                    amount = 1000,
                    //mobile = order.User.MobileNumber,
                    //description = order.User.Description,
                    order_id = orderId,//order.Token == null ? DateTime.Now.Ticks : order.Token,
                    callback = $"https://localhost:44328/Payment/AfterGateway"
                };
                var content = new StringContent(JsonConvert.SerializeObject(model));
                var callRep = await http.PostAsync(hillapayUrl, content);
                if (callRep.IsSuccessStatusCode)
                {
                    var strRep = await callRep.Content.ReadAsStringAsync();
                    var rep = JsonConvert.DeserializeObject<HillaPayGetTransResponse>(strRep);
                    //TODO:Update Order TransId
                    temp.TransId = rep.result_transaction_send.transaction_id;
                    if (rep.status.status == 200) return Json(new
                    {
                        IsSuccessful = true,
                        Result = new
                        {
                            Id = rep.result_transaction_send.transaction_id,
                            Url = rep.result_transaction_send.transaction_url,
                            BasketChanged = true,
                            ChangedProducts = new List<ProductDTO> {
                                new ProductDTO
                                {
                                    Id = 1,
                                    Discount=3,
                                    Price = 40000,
                                    MaxCount = 999
                                }
                            }
                        }
                    });

                    else return Json(new { IsSuccessful = false, Message = "خطایی رخ داده است، دوباره تلاش نمایید" });
                }
                else return Json(new { IsSuccessful = false, Message = "خطایی رخ داده است، دوباره تلاش نمایید" });
            }
        }
    }
}