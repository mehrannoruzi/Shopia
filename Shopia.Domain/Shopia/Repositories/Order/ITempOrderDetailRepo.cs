﻿using Elk.Core;
using System;
using System.Collections.Generic;

namespace Shopia.Domain
{
    public interface ITempOrderDetailRepo : IGenericRepo<TempOrderDetail>, IScopedInjection
    {
        PagingListDetails<TempOrderDetailModel> GetBaskets(TempOrderDetailSearchFilter filter);
        IResponse<IList<ProductDTO>> GetItems(Guid basketId);
    }
}
