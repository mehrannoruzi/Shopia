using System;
using Elk.AspNetCore;
using Shopia.DependencyResolver;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Shopia.Crawler
{
    public class Startup
    {
        private IConfiguration _config { get; }

        private SwaggerSetting _swaggerSetting;

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
            _swaggerSetting = new SwaggerSetting
            {
                Name = "Shopia",
                Title = "Shopia.Api",
                Version = "v1.0",
                Description = $"Copyright � {DateTime.Now.Year} Hillavas Company. All rights reserved.",
                TermsOfService = "https://cdn-hub.ir/",
                JsonUrl = "/swagger/v1/swagger.json",
                Contact = new SwaggerContact
                {
                    Name = "Mehran Norouzi",
                    Email = "M.Norouzi@Hillavas.com",
                    Url = "https://hillavas.com/"
                },
                License = new SwaggerLicense
                {
                    Name = "Hillavas Service Licence",
                    Url = "https://hillavas.com/applicense"
                }
            };
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;
                option.ReturnHttpNotAcceptable = true;
                // option.Filters.Add(typeof(ModelValidationFilter));
            })
            .AddXmlSerializerFormatters()
            .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

            //services.AddMemoryCache();

            services.AddSwagger(_swaggerSetting);

            services.AddTransient(_config);
            services.AddScoped(_config);
            services.AddSingleton(_config);

            services.AddHostedService<QuartzHostedService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseRouting();
            //app.UseExceptionHandler(errorApp =>
            //{
            //    errorApp.Run(async context =>
            //    {
            //        var errorhandler = context.Features.Get<IExceptionHandlerPathFeature>();
            //        context.Response.StatusCode = 500;
            //        context.Response.ContentType = "application/Json";
            //        var bytes = System.Text.Encoding.ASCII.GetBytes(new { IsSuccessful = false, errorhandler.Error?.Message }.ToString());
            //        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
            //    });
            //});

            app.UseAuthorization();

            app.UseSwaggerConfiguration(_swaggerSetting);

            app.UseMvc(routeConfig =>
            {
                routeConfig.MapRoute("Crawl", "{controller=Crawler}/{action=Store}/{Username?}");
                routeConfig.MapRoute("Get", "{controller=Crawler}/{action=Store}/{Username}/{PageNumber}/{PageSize}");
            });
        }
    }
}
