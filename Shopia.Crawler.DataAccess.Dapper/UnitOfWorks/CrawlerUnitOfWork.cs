using System;
using Shopia.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Shopia.Crawler.DataAccess.Dapper
{
    public sealed class CrawlerUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;

        public CrawlerUnitOfWork(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public IPageRepo PageRepo => _serviceProvider.GetService<IPageRepo>();
        public IPostRepo PostRepo => _serviceProvider.GetService<IPostRepo>();
    }
}
