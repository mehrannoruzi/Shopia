using Shopia.Domain;
using System.Threading.Tasks;
using Elk.EntityFrameworkCore;

namespace Shopia.DataAccess.Ef
{
    public class UserRepo : EfGenericRepo<User>, IUserRepo
    {
        public UserRepo(AppDbContext appContext) : base(appContext)
        { }

        public async Task<User> FindByMobileNumber(long mobileNumber) => await FirstOrDefaultAsync(conditions: x => x.MobileNumber == mobileNumber);
    }
}
