using Shopia.Domain;
using Elk.EntityFrameworkCore;

namespace Shopia.DataAccess.Ef
{
    public class TagRepo : EfGenericRepo<Tag>, ITagRepo
    {
        public TagRepo(AppDbContext appContext) : base(appContext)
        { }
    }
}