using System.Threading.Tasks;

namespace Shopia.Domain
{
    public interface IPageRepo
    {
        Task<bool> AddAsync(CrawledPageDto model);
        Task<Page> FindAsync(string pageId);
    }
}
