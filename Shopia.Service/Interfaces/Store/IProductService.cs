using Elk.Core;
using Shopia.Domain;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public interface IProductService
    {
        Task<IResponse<PagingListDetails<ProductDTO>>> Get(ProductFilterDTO filter);

        Task<IResponse<ProductDTO>> FindAsync(int id);
    }
}