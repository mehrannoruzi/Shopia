﻿using Elk.Core;
using Elk.Cache;
using Shopia.Domain;
using Shopia.Service;
using Shopia.DataAccess.Ef;
using Shopia.DataAccess.Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shopia.DependencyResolver
{
    public static class ShopiaDiExtension
    {
        public static IServiceCollection AddTransient(this IServiceCollection serviceCollection, IConfiguration _configuration)
        {
            //serviceCollection.AddTransient<IUserRepo, UserRepo>();

            return serviceCollection;
        }

        public static IServiceCollection AddScoped(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddContext<AppDbContext>(_configuration.GetConnectionString("AppDbContext"));
            services.AddContext<AuthDbContext>(_configuration.GetConnectionString("AuthDbContext"));

            //services.AddScoped<AppDbContext>();
            //services.AddScoped<AuthDbContext>();

            services.AddScoped<AuthUnitOfWork, AuthUnitOfWork>();
            services.AddScoped<AppUnitOfWork, AppUnitOfWork>();

            services.AddScoped(typeof(IGenericRepo<>), typeof(AppGenericRepo<>));
            //services.AddScoped(typeof(IGenericRepo<>), typeof(AuthGenericRepo<>));

            #region Auth

            services.AddScoped<IGenericRepo<Role>, RoleRepo>();
            services.AddScoped<IGenericRepo<Action>, ActionRepo>();
            services.AddScoped<IGenericRepo<ActionInRole>, ActionInRoleRepo>();
            services.AddScoped<IGenericRepo<UserInRole>, UserInRoleRepo>();
            
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IActionService, ActionService>();
            services.AddScoped<IActionInRoleService, ActionInRoleService>();
            services.AddScoped<IUserInRoleService, UserInRoleService>();
            
            services.AddScoped<IUserActionProvider, UserService>();
            
            #endregion


            #region Base

            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<ITagRepo, TagRepo>();
            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IGatewayFactory, GatewayFactory>();
            services.AddScoped<IGatewayService, HillaPayService>(); 
            #endregion
            #region Order
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IDeliveryService, DeliveryService>();
            services.AddScoped<ITempOrderDetailService, TempOrderDetailService>();
            services.AddScoped<ITempOrderDetailRepo, TempOrderDetailRepo>();
            #endregion
            #region Payment
            services.AddScoped<IPaymentRepo, PaymentRepo>();
            services.AddScoped<IPaymentService, PaymentService>();
            #endregion
            #region Store
            //services.AddScoped<IStoreRepo, StoreRepo>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductAssetService, ProductAssetService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            //services.AddScoped<IDiscountRepo, DiscountRepo>();
            #endregion
            #region Dapper Sp

            services.AddScoped<DashboardMenuSp>(factory => new DashboardMenuSp(_configuration.GetConnectionString("AuthDbContext")));

            #endregion

            return services;
        }

        public static IServiceCollection AddSingleton(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddSingleton<IMemoryCacheProvider, MemoryCacheProvider>();

            services.AddSingleton<IEmailService>(s => new EmailService(
                _configuration["CustomSettings:EmailServiceConfig:EmailHost"],
                _configuration["CustomSettings:EmailServiceConfig:EmailUserName"],
                _configuration["CustomSettings:EmailServiceConfig:EmailPassword"]));
            return services;
        }

        public static IServiceCollection AddContext<TDbContext>(this IServiceCollection serviceCollection, string conectionString) where TDbContext : DbContext
        {
            serviceCollection.AddDbContext<TDbContext>(optionBuilder =>
            {
                optionBuilder.UseSqlServer(conectionString,
                            sqlServerOption =>
                            {
                                sqlServerOption.MaxBatchSize(1000);
                                sqlServerOption.CommandTimeout(null);
                                sqlServerOption.UseRelationalNulls(false);
                                //sqlServerOption.EnableRetryOnFailure();
                                //sqlServerOption.UseRowNumberForPaging(false);
                            });
                //.AddInterceptors(new DbContextInterceptors());
            });

            //serviceCollection.AddDbContextPool<TDbContext>(optionBuilder =>
            //{
            //    optionBuilder.UseSqlServer(conectionString,
            //                sqlServerOption =>
            //                {
            //                    sqlServerOption.MaxBatchSize(1000);
            //                    sqlServerOption.CommandTimeout(null);
            //                    //sqlServerOption.EnableRetryOnFailure();
            //                    sqlServerOption.UseRelationalNulls(false);
            //                    //sqlServerOption.UseRowNumberForPaging(false);
            //                });
            //    //.AddInterceptors(new DbContextInterceptors());
            //});

            return serviceCollection;
        }
    }
}
