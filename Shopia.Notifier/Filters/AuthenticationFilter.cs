﻿using System;
using Elk.Core;
using Shopia.Domain;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shopia.Notifier.DataAccess.Dapper;

namespace Shopia.Notifier.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.Headers["Token"].Count > 0)
            {
                if (Guid.TryParse(filterContext.HttpContext.Request.Headers["Token"][0], out Guid token))
                {
                    var applicationRepo = (ApplicationRepo)filterContext.HttpContext.RequestServices.GetService(typeof(IApplicationRepo));
                    var application = applicationRepo.GetAsync(token).Result;
                    if (application != null)
                    {
                        if (filterContext.ActionArguments.ContainsKey("Application"))
                            filterContext.ActionArguments["Application"] = application;

                        base.OnActionExecuting(filterContext);
                    }
                    else
                    {
                        FileLoger.Info($"Invalid Token To Access Api  !" + Environment.NewLine +
                            "IP:{filterContext.HttpContext?.Connection?.RemoteIpAddress?.ToString()}" + Environment.NewLine +
                            $"Token:{filterContext.HttpContext.Request.Headers["Token"][0]}");

                        filterContext.HttpContext.Response.StatusCode = 200;
                        filterContext.Result = new JsonResult(new
                        {
                            Message = "UnAuthorized Access. Invalid Token To Access Api.",
                            Result = 200,
                            IsSuccessful = false
                        });
                    }
                }
                else
                {
                    FileLoger.Info($"Invalid Token To Access Api  !" + Environment.NewLine +
                            "IP:{filterContext.HttpContext?.Connection?.RemoteIpAddress?.ToString()}" + Environment.NewLine +
                            $"Token:{filterContext.HttpContext.Request.Headers["Token"][0]}");

                    filterContext.HttpContext.Response.StatusCode = 200;
                    filterContext.Result = new JsonResult(new
                    {
                        Message = "UnAuthorized Access. Invalid Token To Access Api.",
                        Result = 200,
                        IsSuccessful = false
                    });
                }
            }
            else
            {
                FileLoger.Info($"UnAuthorized Access To Api ! " + Environment.NewLine +
                    "IP:{filterContext.HttpContext?.Connection?.RemoteIpAddress?.ToString()}" + Environment.NewLine +
                    $"Token:");

                filterContext.HttpContext.Response.StatusCode = 403;
                filterContext.Result = new JsonResult(new
                {
                    Message = "UnAuthorized Access. Token Not Sent.",
                    Result = 403,
                    IsSuccessful = false
                });
            }
        }
    }
}
