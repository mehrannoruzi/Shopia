using Shopia.Domain;
using Elk.EntityFrameworkCore;

namespace Shopia.DataAccess.Ef
{
    public class ProductRepo : EfGenericRepo<Product>, IProductRepo
    {
        public ProductRepo(AppDbContext appContext) : base(appContext)
        { }

    }
}
