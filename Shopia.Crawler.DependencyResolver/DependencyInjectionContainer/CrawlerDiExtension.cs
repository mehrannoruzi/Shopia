using Quartz;
using Elk.Core;
using Quartz.Spi;
using Quartz.Impl;
using Shopia.Domain;
using Shopia.DataAccess.Ef;
using Shopia.Crawler.Service;
using Elk.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shopia.Crawler.DataAccess.Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shopia.DependencyResolver
{
    public static class CrawlerDiExtension
    {
        public static IServiceCollection AddTransient(this IServiceCollection serviceCollection, IConfiguration _configuration)
        {
            //serviceCollection.AddTransient<IUserRepo, UserRepo>();

            return serviceCollection;
        }

        public static IServiceCollection AddScoped(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddContext<CrawlerDbContext>(_configuration.GetConnectionString("CrawlerDbContext"));

            services.AddScoped<CrawlerUnitOfWork, CrawlerUnitOfWork>();

            services.AddScoped(typeof(IGenericRepo<>), typeof(EfGenericRepo<>));


            #region Crawler

            services.AddScoped<IPageRepo, PageRepo>();
            services.AddScoped<IPostRepo, PostRepo>();

            services.AddScoped<ICrawlerService, CrawlerService>();

            #endregion

            return services;
        }

        public static IServiceCollection AddSingleton(this IServiceCollection services, IConfiguration _configuration)
        {
            //services.AddSingleton<IMemoryCacheProvider, MemoryCacheProvider>();
            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<UpdatePageAndCrawlNewPost>();
            services.AddSingleton(new JobSchedule(
                                    jobType: typeof(UpdatePageAndCrawlNewPost),
                                    cronExpression: _configuration["InstagramSetting:UpdatePostCronPattern"]));


            services.AddSingleton<InstagramSetting>(new InstagramSetting(
                _configuration["InstagramSetting:PageUrlPattern"],
                _configuration["InstagramSetting:PostUrlPattern"],
                _configuration["InstagramSetting:QueryHash"],
                int.Parse(_configuration["InstagramSetting:MaxCrawledPost"]),
                int.Parse(_configuration["InstagramSetting:CrawledPostPageSize"]),
                _configuration["InstagramSetting:UpdatePostCronPattern"]));

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
