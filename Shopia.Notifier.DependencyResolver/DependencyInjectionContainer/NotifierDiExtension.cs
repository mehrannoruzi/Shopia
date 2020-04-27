﻿using Quartz;
using Quartz.Spi;
using Quartz.Impl;
using Shopia.Domain;
using Shopia.Notifier.Service;
using Microsoft.EntityFrameworkCore;
using Shopia.Notifier.DataAccess.Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shopia.Notifier.DataAccess.Ef;

namespace Shopia.Notifier.DependencyResolver
{
    public static class NotifierDiExtension
    {
        public static IServiceCollection AddTransient(this IServiceCollection serviceCollection, IConfiguration _configuration)
        {
            //serviceCollection.AddTransient<IUserRepo, UserRepo>();

            return serviceCollection;
        }

        public static IServiceCollection AddScoped(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddContext<NotifierDbContext>(_configuration.GetConnectionString("NotifierDbContext"));

            services.AddScoped<NotifierUnitOfWork, NotifierUnitOfWork>();


            #region Notifier

            services.AddScoped<IEventMapperRepo, EventMapperRepo>();
            services.AddScoped<INotificationRepo, NotificationRepo>();

            services.AddScoped<INotificationService, NotificationService>();

            #endregion

            return services;
        }

        public static IServiceCollection AddSingleton(this IServiceCollection services, IConfiguration _configuration)
        {
            //services.AddSingleton<IMemoryCacheProvider, MemoryCacheProvider>();
            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<NotificationProcess>();
            services.AddSingleton(new JobSchedule(
                                    jobType: typeof(NotificationProcess),
                                    cronExpression: _configuration["NotifierSettings:NotificationProcessPattern"]));


            services.AddSingleton<NotifierSetting>(new NotifierSetting(
                _configuration["NotifierSettings:NotifierUrl"],
                _configuration["NotifierSettings:NotificationProcessPattern"],
                _configuration["NotifierSettings:EmailServiceConfig:EmailHost"],
                _configuration["NotifierSettings:EmailServiceConfig:EmailUserName"],
                _configuration["NotifierSettings:EmailServiceConfig:EmailPassword"]));

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

            return serviceCollection;
        }
    }
}
