﻿using Elk.Core;
using Microsoft.AspNetCore.Http;
using Shopia.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public interface IProductService
    {
        Task<IResponse<PagingListDetails<ProductDTO>>> Get(ProductFilterDTO filter);
        Task<IResponse<Product>> FindAsync(int id);
        Task<IResponse<ProductDTO>> FindAsDtoAsync(int id);

        Task<(bool Changed, IEnumerable<OrderItemDTO> Items)> CheckChanges(IEnumerable<OrderItemDTO> items);

        Task<IResponse<Product>> AddAsync(ProductAddModel model);

        Task<IResponse<Product>> FindWithAssetsAsync(int id);

        Task<IResponse<Product>> UpdateAsync(ProductAddModel model);

        Task<IResponse<bool>> DeleteAsync(string baseDomain, string root, int id);

        PagingListDetails<Product> Get(ProductSearchFilter filter);

        Task<IResponse<int>> AddRangeAsync(ProductAddRangeModel model);
        Task<IResponse<List<Post>>> GetPosts(string username, int pageNumber);
    }
}