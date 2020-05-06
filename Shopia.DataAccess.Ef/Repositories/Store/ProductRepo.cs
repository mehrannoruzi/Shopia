using Shopia.Domain;
using Elk.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Shopia.DataAccess.Ef
{
    public class ProductRepo : EfGenericRepo<Product>
    {
        readonly AppDbContext _appContext;
        public ProductRepo(AppDbContext appContext) : base(appContext)
        {
            _appContext = appContext;
        }

        public async Task<IEnumerable<ProductDTO>> GetList(ProductFilterDTO filter)
        {
            var store = _appContext.Set<Store>().FirstOrDefaultAsync(x => x.StoreId == filter.StoreId && x.IsActive);
            if (store == null) return null;
            var dt = new DateTime();
            var discount = await _appContext.Set<Discount>().FirstOrDefaultAsync(x => x.StoreId == filter.StoreId && x.ValidFromDateMi <= dt && x.ValidToDateMi >= dt);
            if (store == null) return null;
            var products = _appContext.Set<Product>().Where(x => x.StoreId == filter.StoreId)
                .Include(x => x.ProductAssets).Select(p => new ProductDTO
                {
                    Id = p.ProductId,
                    Price = p.Price,
                    Discount = p.DiscountPercent,
                    Name = p.Name,
                    ImageUrl = p.ProductAssets == null ? null : p.ProductAssets[0].CdnFileUrl,
                    Description = p.Description
                });

        }

    }
}
