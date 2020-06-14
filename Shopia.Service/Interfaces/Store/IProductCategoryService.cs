using Elk.Core;
using Shopia.Domain;
using System.Collections.Generic;

namespace Shopia.Service
{
    public interface IProductCategoryService
    {
        PagingListDetails<ProductCategory> Get(ProductCategorySearchFilter filter);
        IList<NestedItem> GetAll(ProductCategorySearchFilter filter);
        IDictionary<object, object> Search(string searchParameter, int take = 10);
    }
}