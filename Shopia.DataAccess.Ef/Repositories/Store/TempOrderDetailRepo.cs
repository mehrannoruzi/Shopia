using System;
using Elk.Core;
using System.Linq;
using Shopia.Domain;
using Elk.EntityFrameworkCore;

namespace Shopia.DataAccess.Ef
{
    public class TempOrderDetailRepo : EfGenericRepo<TempOrderDetail>, ITempOrderDetailRepo
    {
        readonly private AppDbContext _appContext;
        public TempOrderDetailRepo(AppDbContext appContext) : base(appContext)
        {
            _appContext = appContext;
        }

        public PagingListDetails<TempOrderDetailDTO> GetBaskets(TempOrderDetailSearchFilter filter)
        {
            var q = _appContext.Set<TempOrderDetail>().AsQueryable();
            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.FromDateSh))
                {
                    var dt = PersianDateTime.Parse(filter.FromDateSh).ToDateTime();
                    q = q.Where(x => x.InsertDateMi >= dt);
                }
                if (!string.IsNullOrWhiteSpace(filter.ToDateSh))
                {
                    var dt = PersianDateTime.Parse(filter.FromDateSh).ToDateTime();
                    q = q.Where(x => x.InsertDateMi <= dt);
                }
                if (filter.BasketId != null)
                {
                    var isGuid = Guid.TryParse(filter.BasketId, out Guid id);
                    if (isGuid) q = q.Where(x => x.BasketId == id);
                }
            }
            var groups = q.GroupBy(x => new
            {
                x.BasketId,
                x.InsertDateSh
            })
            .Select(x => new TempOrderDetailDTO
            {
                BasketId = x.Key.BasketId,
                InsertDateSh = x.Key.InsertDateSh,
                TotalPrice = x.Sum(i => i.TotalPrice)
            });
            var items = groups.OrderByDescending(x => x.InsertDateSh)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();
            var count = q.Count();
            return new PagingListDetails<TempOrderDetailDTO>
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                Items = new PagingList<TempOrderDetailDTO>(items, count, filter),
                TotalCount = count
            };

        }
    }
}
