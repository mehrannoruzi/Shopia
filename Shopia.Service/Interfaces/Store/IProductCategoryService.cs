using Elk.Core;
using Shopia.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public interface IProductCategoryService
    {
        PagingListDetails<ProductCategory> Get(ProductCategorySearchFilter filter);
        IList<ProductCategory> GetAll(ProductCategorySearchFilter filter);
        IDictionary<object, object> Search(string searchParameter, int take = 10);
        Task<IResponse<ProductCategory>> AddAsync(ProductCategory model);
        Task<IResponse<ProductCategory>> UpdateAsync(ProductCategory model);
        Task<IResponse<bool>> DeleteAsync(int id);
        Task<IResponse<ProductCategory>> FindAsync(int id);
    }
}