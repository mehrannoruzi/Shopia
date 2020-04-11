using System;
using Elk.Core;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Shopia.Domain
{
    public interface IPageRepo
    {
        Task<bool> AddAsync(CrawledPageDto model);
        Task<bool> UpdateAsync(CrawledPageDto model);
        Task<bool> DeleteAsync(string pageId);
        Task<Page> FindAsync(string pageId);
        Task<IEnumerable<Page>> GetAsync(DateTime lastCrawlDate, PagingParameter pagingParameter);
    }
}