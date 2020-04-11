using Quartz;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Shopia.Crawler.Service
{
    [DisallowConcurrentExecution]
    public class UpdatePageAndCrawlNewPost : IJob
    {
        private IServiceProvider _serviceProvider { get; }

        public UpdatePageAndCrawlNewPost(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public Task Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var crawlerService = scope.ServiceProvider.GetService<ICrawlerService>();
                crawlerService.UpdatePageAndCrawlNewPostAsync();
            }

            return Task.CompletedTask;
        }
    }
}
