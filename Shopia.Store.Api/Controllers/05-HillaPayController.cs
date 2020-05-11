using Elk.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shopia.Domain;
using Shopia.Service;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopia.Store.Api.Controllers
{
    public class HillaPayController : Controller
    {
        readonly IOrderService _orderSrv;
        public HillaPayController(IOrderService orderSrv)
        {
            _orderSrv = orderSrv;
        }
        [HttpPost]
        public async Task<IActionResult> AfterGateway(HillaPayAfterGatewayModel model)
        {
            if (model.Status.status != 400)
                return RedirectToAction("ShowResult", "Payment", new Response<string> { IsSuccessful = false, Result = model.result_transaction_callback.transaction_id });
            else
            {
                var apiKey = "xkeysib1";
                var verifyUrl = "https://api.hillapay.ir/ipg/v2/verify";
                //TODO:find order by transid
                using (var http = new HttpClient())
                {
                    http.DefaultRequestHeaders.Add("api-key", apiKey);
                    var contentModel = new
                    {
                        order_id = Constant.Instance().OrderId,
                        transaction_id = Constant.Instance().TransId,
                        rrn = model.result_transaction_callback.rrn
                    };
                    var content = new StringContent(JsonConvert.SerializeObject(contentModel));
                    var callRep = await http.PostAsync(verifyUrl, content);
                    if (callRep.IsSuccessStatusCode)
                    {
                        var strRep = await callRep.Content.ReadAsStringAsync();
                        var rep = JsonConvert.DeserializeObject<HillaPayVerifyReponse>(strRep);
                        //TODO:Update Order TransId

                        if (rep.status.status == 500)
                            return RedirectToAction("ShowResult", "Payment", new Response<string> { IsSuccessful = true, Result = rep.result_transaction_verify.transaction_id });
                        else return RedirectToAction("ShowResult", "Payment", new Response<string> { IsSuccessful = false, Result = rep.result_transaction_verify.transaction_id });
                    }
                    return RedirectToAction("ShowResult", "Payment", new Response<string> { IsSuccessful = false, Result = "" });
                }
            }

        }

        [HttpGet]
        public IActionResult ShowResult(Response<string> model)
        {
            return Redirect($"http://localhost:3000/afterGateway/{Convert.ToByte(model.IsSuccessful)}/{model.Result}");
        }
    }
}