using Elk.EntityFrameworkCore;

namespace Shopia.DataAccess.Ef
{
    public class AppGenericRepo<T> : EfGenericRepo<T> where T : class
    {
        public AppGenericRepo(AppDbContext appDbContext) : base(appDbContext) { }
    }
}