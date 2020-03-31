using Shopia.Domain;
using Elk.EntityFrameworkCore;

namespace Shopia.DataAccess.Ef
{
    public class ActionRepo : EfGenericRepo<Action>
    {
        public ActionRepo(AuthDbContext authContext) : base(authContext)
        { }
    }
}