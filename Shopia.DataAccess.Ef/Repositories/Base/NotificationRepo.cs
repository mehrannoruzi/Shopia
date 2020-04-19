using Shopia.Domain;
using Elk.EntityFrameworkCore;

namespace Shopia.DataAccess.Ef
{
    public class NotificationRepo : EfGenericRepo<Notification>
    {
        public NotificationRepo(AppDbContext appContext) : base(appContext)
        { }


    }
}
