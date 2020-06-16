using Elk.Core;
using System.Linq;
using Shopia.Domain;
using Shopia.DataAccess.Ef;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;
using Shopia.Service.Resource;

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

        public IList<ProductCategory> GetAll(ProductCategorySearchFilter filter)
        {
            Expression<Func<ProductCategory, bool>> conditions = x => true;
            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.Name))
                    conditions = x => x.Name.Contains(filter.Name);
            }
            return _productCategoryRepo.Get(conditions: conditions, orderBy: x => x.OrderByDescending(u => u.ProductCategoryId));
        }

        public IDictionary<object, object> Search(string searchParameter, int take = 10)
                => _productCategoryRepo.Get(conditions: x => x.Name.Contains(searchParameter))
                .OrderByDescending(x => x.Name)
                .Take(take)
                .ToDictionary(k => (object)k.ProductCategoryId, v => (object)v.Name);

        public async Task<IResponse<ProductCategory>> FindAsync(int id)
        {
            var item = await _productCategoryRepo.FindAsync(id);
            if (item == null) return new Response<ProductCategory> { Message = ServiceMessage.RecordNotExist };

            return new Response<ProductCategory> { Result = item, IsSuccessful = true };
        }

        public async Task<IResponse<ProductCategory>> AddAsync(ProductCategory model)
        {
            await _productCategoryRepo.AddAsync(model);

            var saveResult = await _appUow.ElkSaveChangesAsync();
            return new Response<ProductCategory> { Result = model, IsSuccessful = saveResult.IsSuccessful, Message = saveResult.Message };
        }

        public async Task<IResponse<ProductCategory>> UpdateAsync(ProductCategory model)
        {
            var findedRole = await _productCategoryRepo.FindAsync(model.ProductCategoryId);
            if (findedRole == null) return new Response<ProductCategory> { Message = ServiceMessage.RecordNotExist };

            findedRole.Name = model.Name;
            findedRole.Icon = model.Icon;
            findedRole.OrderPriority = model.OrderPriority;

            var saveResult = await _appUow.ElkSaveChangesAsync();
            return new Response<ProductCategory> { Result = findedRole, IsSuccessful = saveResult.IsSuccessful, Message = saveResult.Message };
        }

        public async Task<IResponse<bool>> DeleteAsync(int id)
        {
            _productCategoryRepo.Delete(new ProductCategory { ProductCategoryId = id });
            var saveResult = await _appUow.ElkSaveChangesAsync();
            return new Response<bool>
            {
                Message = saveResult.Message,
                Result = saveResult.IsSuccessful,
                IsSuccessful = saveResult.IsSuccessful,
            };
        }
    }
}
