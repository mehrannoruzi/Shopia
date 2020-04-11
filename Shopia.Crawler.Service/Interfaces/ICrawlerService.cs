using Elk.Core;
using Shopia.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Shopia.Crawler.Service
{
    public interface ICrawlerService
    {
        Task<IResponse<CrawledPageDto>> CrawlPageAsync(string username);
        Task<IResponse<bool>> CrawlPostAsync(string username);
        Task<IResponse<bool>> CrawlNewPostAsync(Page page, int newPostCount);
        Task<IResponse<IEnumerable<Post>>> GetPostAsync(string username, PagingParameter pagingParameter);
    }
}
