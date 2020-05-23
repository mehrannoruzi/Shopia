using Shopia.Domain;
using Elk.EntityFrameworkCore;

namespace Shopia.DataAccess.Ef
{
    public class UserInRoleRepo : EfGenericRepo<UserInRole>
    {
        public UserInRoleRepo(AuthDbContext authContext) : base(authContext)
        { }
    }
}
