using Elk.Core;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Shopia.Domain
{
    public interface IPostRepo
    {
        Task<bool> AddAsync(CrawledPostDto model);
        Task<IEnumerable<Post>> GetAsync(string username, PagingParameter pagingParameter);
    }
}
