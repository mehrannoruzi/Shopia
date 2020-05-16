﻿using Elk.Core;
using Shopia.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public interface IProductService
    {
        Task<IResponse<PagingListDetails<ProductDTO>>> Get(ProductFilterDTO filter);

        Task<IResponse<ProductDTO>> FindAsync(int id);

        Task<(bool Changed, IEnumerable<OrderItemDTO> Items)> CheckChanges(IEnumerable<OrderItemDTO> items);

        Task<IResponse<Product>> AddAsync(Product model);

        Task<IResponse<Product>> FindWithAssetsAsync(int id);

        Task<IResponse<Product>> UpdateAsync(Product model);

        Task<IResponse<bool>> DeleteAsync(int id);

        PagingListDetails<Product> Get(ProductSearchFilter filter);
    }
}