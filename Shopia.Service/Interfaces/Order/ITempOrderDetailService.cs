using System;
using Elk.Core;
using Shopia.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Shopia.Service
{
    public interface ITempOrderDetailService
    {
        Task<IResponse<Guid>> AddRangeAsync(IList<TempOrderDetail> model);
        Task<IResponse<bool>> DeleteAsync(Guid basketId);
        PagingListDetails<TempOrderDetailModel> Get(TempOrderDetailSearchFilter filter);
        List<TempOrderDetail> GetDetails(Guid basketId);
        IResponse<IList<ProductDTO>> Get(Guid basketId);
    }
}