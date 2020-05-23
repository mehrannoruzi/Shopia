using Elk.EntityFrameworkCore;

namespace Shopia.DataAccess.Ef
{
    public class AuthGenericRepo<T> : EfGenericRepo<T> where T : class
    {
        public AuthGenericRepo(AuthDbContext authDbContext) : base(authDbContext) { }
    }
}