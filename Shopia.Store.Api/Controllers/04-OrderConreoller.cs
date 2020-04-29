using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

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

        [HttpPost,EnableCors]
        public IActionResult Submit([FromBody]OrderDTO order)
        {
            return Json(new { IsSuccessful = true,Result = "gateway url" });
        }

      //[HttpPost]
      //  public IActionResult Verify(int orderId)
      //  {
      //      return Json(new
      //      {
      //          IsSuccessful = true,
      //          Result = new
      //          {
      //              IsVerfied = true,
      //              TraceId = "1234567"
      //          }
      //      });
      //  }
    }
}