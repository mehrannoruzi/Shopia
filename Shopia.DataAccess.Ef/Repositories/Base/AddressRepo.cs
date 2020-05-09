using Shopia.Domain;
using Elk.EntityFrameworkCore;

namespace Shopia.DataAccess.Ef
{
    public class AddressRepo : EfGenericRepo<Address>, IAddressRepo
    {
        readonly AppDbContext _appContext;
        public AddressRepo(AppDbContext appContext) : base(appContext)
        {
            _appContext = appContext;
        }

    }
}
