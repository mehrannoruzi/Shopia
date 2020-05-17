using System;
using Elk.Core;
using Shopia.Domain;
using Shopia.Service;
using Elk.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Shopia.Dashboard.Resources;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Shopia.DataAccess.Ef;
using Elk.AspNetCore;

namespace Shopia.Dashboard.Controllers
{
    public partial class AuthController : Controller
    {
        private readonly IUserService _userSrv;
        private IConfiguration _config { get; set; }
        private readonly IHttpContextAccessor _httpAccessor;
        private const string UrlPrefixKey = "CustomSettings:UrlPrefix";

        private readonly AuthDbContext _db;

        public AuthController(IHttpContextAccessor httpAccessor, IConfiguration configuration,
            IUserService userSrv, AuthDbContext db)
        {
            _userSrv = userSrv;
            _config = configuration;
            _httpAccessor = httpAccessor;
            _db = db;
        }

        private async Task CreateCookie(User user, bool remeberMe)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Fullname", user.FullName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8),
                IsPersistent = remeberMe,
            };
            await _httpAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        [HttpGet]
        public virtual ActionResult SignIn()
        {
            //var t = new AclSeed(_db);
            //var rep = t.Init();

            if (User.Identity.IsAuthenticated)
            {
                var urlPrefix = _config.GetValue<string>(UrlPrefixKey);
                var defaultUA =  _userSrv.GetAvailableActions(User.GetUserId(), null, urlPrefix).DefaultUserAction;
                return Redirect($"{urlPrefix}/{defaultUA.Controller}/{defaultUA.Action}");
            }
            return View(new SignInModel { RememberMe = true });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual async Task<JsonResult> SignIn(SignInModel model)
        {
            if (!ModelState.IsValid) return Json(new Response<string> { IsSuccessful = false, Message = ModelState.GetModelError() });
            if (!long.TryParse(model.Mobilenumber, out long mobNum)) return Json(new Response<string> { IsSuccessful = false, Message = ModelState.GetModelError() });

            var chkRep = await _userSrv.Authenticate(mobNum, model.Password);
            if (!chkRep.IsSuccessful) return Json(new Response<string> { IsSuccessful = false, Message = chkRep.Message });

            var menuRep = _userSrv.GetAvailableActions(chkRep.Result.UserId, null, _config.GetValue<string>(UrlPrefixKey));
            if (menuRep == null) return Json(new Response<string> { IsSuccessful = false, Message = Strings.ThereIsNoViewForUser });

            await CreateCookie(chkRep.Result, model.RememberMe);
            return Json(new Response<string> { IsSuccessful = true, Result = Url.Action(menuRep.DefaultUserAction.Action, menuRep.DefaultUserAction.Controller, new { }), });
        }

        public virtual async Task<ActionResult> SignOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _httpAccessor.HttpContext.SignOutAsync();
            }

            return RedirectToAction("SignIn");
        }

        [HttpGet]
        public virtual ActionResult RecoverPasswrod() => View();

        [HttpPost]
        public virtual async Task<JsonResult> RecoverPasswrod(long mobileNumber)
        {
            var emailModel = new EmailMessage();
            emailModel.Body = await ControllerExtension.RenderViewToStringAsync(this, "Partials/_NewPassword", "");
            return Json(await _userSrv.RecoverPassword(mobileNumber, _config["CustomSettings:EmailServiceConfig:EmailUserName"], emailModel));
        }

    }
}