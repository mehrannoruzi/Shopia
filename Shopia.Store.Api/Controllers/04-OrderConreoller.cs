using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shopia.Domain;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopia.Store.Api.Controllers
{
    public class OrderController : Controller
    {

        [HttpPost]
        public IActionResult CompleteInfo([FromBody]UserDTO model)
        {
            if (model.Token == null)
            {
                //create new user & return token
            }
            else
            {
                //update user info if params changed & return token
            }
            return Json(new
            {
                IsSuccessful = true,
                Result = Guid.NewGuid().ToString()//token
            });
        }

        [HttpPost, EnableCors]
        public async Task<IActionResult> Submit([FromBody]OrderDTO order)
        {
            var apiKey = "xkeysib1";
            var hillapayUrl = "https://api.hillapay.ir/ipg/v3/send";
            if (order.OrderId == null)
            {
                //TODO:Add TO DB AND Calculate Total Price Again
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
                http.DefaultRequestHeaders.Add("api-key", apiKey);
                var model = new
                {
                    amount = 1000,
                    mobile = order.User.MobileNumber,
                    description = order.User.Description,
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