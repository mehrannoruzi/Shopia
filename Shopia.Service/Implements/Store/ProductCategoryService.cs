using Elk.Core;
using System.Linq;
using Shopia.Domain;
using Shopia.DataAccess.Ef;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace Shopia.Service
{
    public class ProductCategoryService : IProductCategoryService
    {
        readonly AppUnitOfWork _appUow;
        readonly IGenericRepo<ProductCategory> _productCategoryRepo;
        public ProductCategoryService(AppUnitOfWork appUOW, IGenericRepo<ProductCategory> productCategoryRepo)
        {
            _appUow = appUOW;
            _productCategoryRepo = productCategoryRepo;
        }

        public PagingListDetails<ProductCategory> Get(ProductCategorySearchFilter filter)
        {
            Expression<Func<ProductCategory, bool>> conditions = x => true;
            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.Name))
                    conditions = x => x.Name.Contains(filter.Name);
            }

            return _productCategoryRepo.Get(conditions, filter, x => x.OrderByDescending(u => u.ProductCategoryId));
        }

        public IDictionary<object, object> Search(string searchParameter, int take = 10)
                => _productCategoryRepo.Get(conditions: x => x.Name.Contains(searchParameter))
                .OrderByDescending(x => x.Name)
                .Take(take)
                .ToDictionary(k => (object)k.ProductCategoryId, v => (object)v.Name);
    }
}
