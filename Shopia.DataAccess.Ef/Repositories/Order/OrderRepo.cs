using Shopia.Domain;
using System.Threading.Tasks;
using Elk.EntityFrameworkCore;

namespace Shopia.DataAccess.Ef
{
    public class OrderRepo : EfGenericRepo<Order>, IOrderRepo
    {
        public OrderRepo(AppDbContext appContext) : base(appContext)
        { }

    }
}
