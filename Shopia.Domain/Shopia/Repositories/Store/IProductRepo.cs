using Elk.Core;
using System.Threading.Tasks;

namespace Shopia.Domain
{
    public interface IProductRepo : IGenericRepo<Product>, IScopedInjection
    {
        Task<IResponse<PagingListDetails<ProductDTO>>> GetAndCalcDiscountAsync(ProductSearchFilter filter);
    }
}
