using Shopia.Domain;
using System.Threading.Tasks;
using Elk.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Elk.Core;

namespace Shopia.DataAccess.Ef
{
    public class UserRepo : EfGenericRepo<User>, IUserRepo
    {
        readonly AppDbContext _appContext;
        public UserRepo(AppDbContext appContext) : base(appContext)
        {
            _appContext = appContext;
        }

        public async Task<User> FindByMobileNumber(long mobileNumber) => await FirstOrDefaultAsync(conditions: x => x.MobileNumber == mobileNumber);//_appContext.Set<User>().FirstOrDefaultAsync(x => x.MobileNumber == mobileNumber);
    }
}
