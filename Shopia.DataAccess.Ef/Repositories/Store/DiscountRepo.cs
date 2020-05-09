using Shopia.Domain;
using Elk.EntityFrameworkCore;

namespace Shopia.DataAccess.Ef
{
    public class DiscountRepo : EfGenericRepo<Discount>, IDiscountRepo
    {
        public DiscountRepo(AppDbContext appContext) : base(appContext)
        { }


    }
}
