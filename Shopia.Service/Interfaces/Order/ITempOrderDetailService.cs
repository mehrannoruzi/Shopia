using System;
using Elk.Core;
using Shopia.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public interface ITempOrderDetailService
    {
        Task<IResponse<Guid>> AddRangeAsync(IList<TempOrderDetail> model);
        Task<IResponse<bool>> DeleteAsync(Guid basketId);
        PagingListDetails<TempOrderDetailDTO> Get(TempOrderDetailSearchFilter filter);
        List<TempOrderDetail> Get(Guid basketId);
    }
}