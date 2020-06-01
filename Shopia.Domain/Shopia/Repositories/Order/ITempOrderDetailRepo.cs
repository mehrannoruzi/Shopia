using Elk.Core;

namespace Shopia.Domain
{
    public interface ITempOrderDetailRepo : IGenericRepo<TempOrderDetail>, IScopedInjection
    {
        PagingListDetails<TempOrderDetailDTO> GetBaskets(TempOrderDetailSearchFilter filter);
    }
}
