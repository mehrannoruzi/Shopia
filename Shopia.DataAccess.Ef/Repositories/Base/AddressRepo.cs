using Shopia.Domain;
using System.Threading.Tasks;
using Elk.EntityFrameworkCore;

namespace Shopia.DataAccess.Ef
{
    public class AddressRepo : EfGenericRepo<Address>, IAddressRepo
    {
        public AddressRepo(AppDbContext appContext) : base(appContext)
        { }

    }
}
