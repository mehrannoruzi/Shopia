using Shopia.Domain;
using Elk.EntityFrameworkCore;
using Elk.Core;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Shopia.DataAccess.Ef
{
    public class ProductRepo : EfGenericRepo<Product>, IProductRepo
    {
        readonly DbSet<Product> _product;
        readonly AppDbContext _appContext;
        public ProductRepo(AppDbContext appContext) : base(appContext)
        {
            _appContext = appContext;
            _product = _appContext.Set<Product>();
        }

        public async Task<IResponse<PagingListDetails<ProductDTO>>> GetAndCalcDiscountAsync(ProductSearchFilter filter)
        {
            var q = _product.Where(x=>!x.IsDeleted).Include(x => x.ProductAssets).AsQueryable().AsNoTracking();
            var currentDT = DateTime.Now;
            var discount = await _appContext.Set<Discount>().FirstOrDefaultAsync(x => x.StoreId == filter.StoreId && x.ValidFromDateMi <= currentDT && x.ValidToDateMi >= currentDT);
            if (filter.StoreId != null)
                q = q.Where(x => x.StoreId == filter.StoreId);
            if (!string.IsNullOrWhiteSpace(filter.Name)) q = q.Where(x => x.Name.Contains(filter.Name));

            switch (filter.Category)
            {
                case ProductFilterCategory.Favorites:
                    q = q.OrderByDescending(x => x.LikeCount);
                    break;
                case ProductFilterCategory.BestSellers:
                    q = q.OrderByDescending(x => x.OrderDetails.Count());
                    break;
                default:
                    q = q.OrderByDescending(x => x.ProductId);
                    break;
            }
            var result = q.Select(p => new ProductDTO
            {
                Id = p.ProductId,
                Price = p.Price,
                Discount = p.DiscountPercent,
                Name = p.Name,
                ImageUrl = p.ProductAssets.Any() ? p.ProductAssets[0].FileUrl : null,
                Description = p.Description
            }).ToPagingListDetails(filter);
            if (discount != null)
            {
                foreach (var p in result.Items)
                {
                    if (p.Discount != null) continue;
                    p.Discount = discount.Percent;
                    var discountAmount = p.Price * p.Discount / 100;
                    if (discountAmount > discount.MaxPrice)
                        p.Discount = (float)Math.Floor((float)(100 * discount.MaxPrice / p.Price));
                }

            }
            return new Response<PagingListDetails<ProductDTO>> {
                IsSuccessful = true,
                Result = result
            };
        }

    }
}
