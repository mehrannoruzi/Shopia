using System;
using Elk.Core;
using System.Linq;
using Shopia.Domain;
using Shopia.DataAccess.Ef;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Shopia.Service.Resource;

namespace Shopia.Service
{
    public class ProductService : IProductService
    {
        readonly AppUnitOfWork _appUow;
        readonly IProductRepo _productRepo;
        public ProductService(AppUnitOfWork appUOW)
        {
            _appUow = appUOW;
            _productRepo = appUOW.ProductRepo;
        }

        public async Task<IResponse<PagingListDetails<ProductDTO>>> Get(ProductFilterDTO filter)
        {
            var currentDT = DateTime.Now;
            var discount = await _appUow.DiscountRepo.FirstOrDefaultAsync(conditions: x => x.StoreId == filter.StoreId && x.ValidFromDateMi <= currentDT && x.ValidToDateMi >= currentDT);
            var products = _productRepo.Get(selector: p => new ProductDTO
            {
                Id = p.ProductId,
                Price = p.Price,
                Discount = p.DiscountPercent,
                Name = p.Name,
                ImageUrl = p.ProductAssets == null ? null : p.ProductAssets[0].ThumbnailUrl,
                Description = p.Description
            },
            conditions: x => x.StoreId == filter.StoreId,
            pagingParameter: filter,
            orderBy: o => o.OrderByDescending(x => x.ProductId));
            if (discount != null)
            {
                foreach (var p in products.Items)
                {
                    p.Discount = discount.Percent;
                    var discountAmount = p.Price * p.Discount / 100;
                    if (discountAmount > discount.MaxPrice)
                        p.Discount = (float)Math.Floor((float)(100 * discount.MaxPrice / p.Price));
                }

            }

            return new Response<PagingListDetails<ProductDTO>>
            {
                Result = products,
                IsSuccessful = true
            };
        }

        public async Task<IResponse<ProductDTO>> FindAsync(int id)
        {
            var product = await _productRepo.FirstOrDefaultAsync(conditions: x => x.ProductId == id && x.IsActive,
                new List<Expression<Func<Product, object>>> { x => x.ProductAssets });
            if (product == null) return new Response<ProductDTO>
            {
                IsSuccessful = false,
                Message = ServiceMessage.RecordNotExist
            };
            var currentDT = DateTime.Now;
            var discount = await _appUow.DiscountRepo.FirstOrDefaultAsync(conditions: x => x.StoreId == product.StoreId && x.ValidFromDateMi <= currentDT && x.ValidToDateMi >= currentDT);
            return new Response<ProductDTO>
            {
                IsSuccessful = true,
                Result = new ProductDTO
                {
                    Id = product.ProductId,
                    Price = product.Price,
                    Discount = product.DiscountPercent,
                    Name = product.Name,
                    ImageUrl = product.ProductAssets == null ? null : product.ProductAssets[0].FileUrl,
                    Description = product.Description
                }
            };

        }
    }
}
