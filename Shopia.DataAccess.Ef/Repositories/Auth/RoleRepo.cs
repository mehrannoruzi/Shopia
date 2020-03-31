using Shopia.Domain;
using Elk.EntityFrameworkCore;

namespace Shopia.DataAccess.Ef
{
    public class RoleRepo : EfGenericRepo<Role>
    {
        public RoleRepo(AuthDbContext authContext) : base(authContext)
        { }
    }
}