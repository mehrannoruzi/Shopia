using System;
using Elk.Core;
using System.Linq;
using Shopia.Domain;
using System.Threading;
using Shopia.DataAccess.Ef;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public class ProductService
    {
        readonly AppUnitOfWork _appUow;
        readonly IProductRepo _productRepo;
        public ProductService(AppUnitOfWork appUOW)
        {
            _appUow = appUOW;
            _productRepo = appUOW.ProductRepo;
        }

        public async Task<IResponse<ProductDTO>> Get(ProductFilterDTO filter)
        {
            var store = await _appUow.StoreRepo.FirstOrDefaultAsync(conditions: x => x.StoreId == filter.StoreId && x.IsActive);
            var cdt = new DateTime();
            var discount = await _appUow.DiscountRepo.FirstOrDefaultAsync(conditions: x => x.StoreId == filter.StoreId && x.ValidFromDateMi <= cdt && x.ValidToDateMi >= cdt);
            var products = _productRepo.Get(selector: p => new ProductDTO
            {
                Id = p.ProductId,
                Price = p.Price,
                Discount = p.DiscountPercent,
                Name = p.Name,
                ImageUrl = p.ProductAssets == null ? null : p.ProductAssets[0].CdnFileUrl,
                Description = p.Description
            },
            conditions: x => x.StoreId == filter.StoreId,
            pagingParameter: filter,
            orderBy: o => o.OrderByDescending(x => x.ProductId));
            foreach (var p in products.Items)
            {
                var discountAmount = p.Price * p.Discount / 100;
                //if(discountAmount>store.max)
            }
            return null;
        }

        public async Task<bool> SuccessCrawlAsync(string UniqueId, CancellationToken token = default)
        {
            //await _appUow.NotificationRepo.AddAsync(model, token);
            var saveResult = await _appUow.ElkSaveChangesAsync(token);
            return saveResult.IsSuccessful;
        }


    }
}
