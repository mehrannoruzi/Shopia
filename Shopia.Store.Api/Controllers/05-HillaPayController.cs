using Elk.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shopia.Domain;
using Shopia.Service;
using System.Threading.Tasks;

namespace Shopia.Store.Api.Controllers
{
    public class HillaPayController : Controller
    {
        readonly IOrderService _orderSrv;
        readonly IPaymentService _paymentSrv;
        readonly IGatewayService _gatewaySrv;
        readonly IConfiguration _configuration;
        public HillaPayController(IOrderService orderSrv, IPaymentService paymentSrv, IGatewayService gatewaySrv, IConfiguration configuration)
        {
            _orderSrv = orderSrv;
            _configuration = configuration;
            _paymentSrv = paymentSrv;
            _gatewaySrv = gatewaySrv;
        }

        [HttpPost]
        public async Task<IActionResult> AfterGateway(HillaPayAfterGatewayModel model)
        {
            var controller = _configuration["ShowPaymentResult:Controller"];
            var action = _configuration["ShowPaymentResult:Action"];
            if (model.Status.status != 400) return RedirectToAction(action, controller, new Response<string> { IsSuccessful = false, Result = model.result_transaction_callback.transaction_id });
            else
            {
                var findPayment = await _paymentSrv.FindAsync(model.result_transaction_callback.transaction_id);
                if (!findPayment.IsSuccessful) return RedirectToAction(action, controller, new Response<string> { IsSuccessful = false, Result = model.result_transaction_callback.transaction_id });
                var verify = await _orderSrv.Verify(findPayment.Result, new object[1] { model.result_transaction_callback.rrn });
                return RedirectToAction(action, controller, new Response<string> { IsSuccessful = verify.IsSuccessful, Result = model.result_transaction_callback.transaction_id });
            }

        }
    }
}