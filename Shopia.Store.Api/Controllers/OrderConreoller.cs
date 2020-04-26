using Microsoft.AspNetCore.Mvc;
using System;

namespace Shopia.Store.Api.Controllers
{
    public class OrderController : Controller
    {

        [HttpPost]
        public IActionResult CompleteInfo(string fullname, string mobilenumber, string description, Guid? token)
        {
            if (token == null)
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
                Result = Guid.NewGuid()//token
            });
        }

        [HttpPost]
        public IActionResult Verify(int orderId)
        {
            return Json(new
            {
                IsSuccessful = true,
                Result = new
                {
                    IsVerfied = true,
                    TraceId = "1234567"
                }
            });
        }
    }
}