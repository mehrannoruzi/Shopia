using System;
using Elk.Core;
using System.Linq;
using Shopia.Domain;
using Shopia.DataAccess.Ef;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Shopia.Service.Resource;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace Shopia.Service
{
    public class ProductService : IProductService
    {
        readonly AppUnitOfWork _appUow;
        readonly IProductAssetService _productAssetService;
        readonly IConfiguration _configuration;
        readonly IGenericRepo<Product> _productRepo;
        readonly IGenericRepo<Discount> _discountRepo;
        public ProductService(AppUnitOfWork appUOW,
            IGenericRepo<Product> productRepo,
            IGenericRepo<Discount> discountRepo,
            IProductAssetService productAssetService,
            IConfiguration configuration)
        {
            _appUow = appUOW;
            _productRepo = productRepo;
            _discountRepo = discountRepo;
            _productAssetService = productAssetService;
            _configuration = configuration;
        }

        public async Task<IResponse<PagingListDetails<ProductDTO>>> Get(ProductFilterDTO filter)
        {
            var currentDT = DateTime.Now;
            var discount = await _discountRepo.FirstOrDefaultAsync(conditions: x => x.StoreId == filter.StoreId && x.ValidFromDateMi <= currentDT && x.ValidToDateMi >= currentDT);
            var products = _productRepo.Get(selector: p => new ProductDTO
            {
                Id = p.ProductId,
                Price = p.Price,
                Discount = p.DiscountPercent,
                Name = p.Name,
                ImageUrl = p.ProductAssets == null ? null : p.ProductAssets[0].ThumbnailUrl,
                Description = p.Description
            },
            conditions: x => x.StoreId == filter.StoreId && !x.IsDeleted,
            pagingParameter: filter,
            orderBy: o => o.OrderByDescending(x => x.ProductId),
            new List<Expression<Func<Product, object>>> { x => x.ProductAssets });
            if (discount != null)
            {
                foreach (var p in products.Items)
                {
                    if (p.Discount != null) continue;
                    p.Discount = discount.Percent;
                    var discountAmount = p.Price * p.Discount / 100;
                    if (discountAmount > discount.MaxPrice)
                        p.Discount = (float)Math.Floor((float)(100 * discount.MaxPrice / p.Price));
                }

            }
            return new Response<PagingListDetails<ProductDTO>>
            {
                Result = products,
                IsSuccessful = true
            };
        }

        public async Task<IResponse<ProductDTO>> FindAsDtoAsync(int id)
        {
            var product = await _productRepo.FirstOrDefaultAsync(conditions: x => x.ProductId == id && x.IsActive,
                new List<Expression<Func<Product, object>>> { x => x.ProductAssets });
            if (product == null) return new Response<ProductDTO>
            {
                IsSuccessful = false,
                Message = ServiceMessage.RecordNotExist
            };
            var currentDT = DateTime.Now;
            var discount = await _discountRepo.FirstOrDefaultAsync(conditions: x => x.StoreId == product.StoreId && x.ValidFromDateMi <= currentDT && x.ValidToDateMi >= currentDT);
            return new Response<ProductDTO>
            {
                IsSuccessful = true,
                Result = new ProductDTO
                {
                    Id = product.ProductId,
                    Price = product.Price,
                    Discount = product.DiscountPercent,
                    Name = product.Name,
                    Description = product.Description,
                    Slides = product.ProductAssets?.Select(x => x.FileUrl).ToList()
                }
            };

        }

        public async Task<IResponse<Product>> FindAsync(int id)
        {
            var product = await _productRepo.FirstOrDefaultAsync(conditions: x => x.ProductId == id,
                new List<Expression<Func<Product, object>>> { x => x.ProductAssets, x => x.Store });
            if (product == null) return new Response<Product> { Message = ServiceMessage.RecordNotExist };
            return new Response<Product>
            {
                IsSuccessful = true,
                Result = product
            };

        }

        public async Task<(bool Changed, IEnumerable<OrderItemDTO> Items)> CheckChanges(IEnumerable<OrderItemDTO> items)
        {
            var products = _productRepo.Get(conditions: x => items.Select(x => x.Id).Contains(x.ProductId),
            orderBy: o => o.OrderByDescending(x => x.ProductId),
            new List<Expression<Func<Product, object>>> { x => x.Store });
            bool changed = false;
            foreach (var item in items)
            {
                var product = products.FirstOrDefault(x => x.ProductId == item.Id);
                if (product == null)
                {
                    changed = true;
                    item.Count = 0;
                    continue;
                }
                var currentDT = DateTime.Now;
                var discount = await _discountRepo.FirstOrDefaultAsync(conditions: x => x.StoreId == product.StoreId && x.ValidFromDateMi <= currentDT && x.ValidToDateMi >= currentDT);
                if (discount != null && product.DiscountPercent == null)
                {
                    product.DiscountPercent = discount.Percent;
                    var discountAmount = product.Price * product.DiscountPercent / 100;
                    if (discountAmount > discount.MaxPrice)
                        product.DiscountPercent = (float)Math.Floor((float)(100 * discount.MaxPrice / product.Price));
                }
                if (item.Price != product.Price || item.Discount != product.DiscountPercent)
                    changed = true;
                //item.MaxCount
                item.Price = product.Price;
                item.Discount = product.DiscountPercent;

            }
            return (changed, items);
        }

        public async Task<IResponse<Product>> AddAsync(ProductAddModel model)
        {
            var product = new Product().CopyFrom(model);
            if (model.Files != null && model.Files.Count != 0)
            {
                var getAssets = await _productAssetService.SaveRange(model);
                if (!getAssets.IsSuccessful) return new Response<Product> { Message = getAssets.Message };
                product.ProductAssets = getAssets.Result;
                await _productRepo.AddAsync(product);
                var add = await _appUow.ElkSaveChangesAsync();
                if (!add.IsSuccessful) _productAssetService.DeleteRange(getAssets.Result);
                return new Response<Product> { Result = product, IsSuccessful = add.IsSuccessful, Message = add.Message };
            }
            else
            {
                await _productRepo.AddAsync(product);
                var add = await _appUow.ElkSaveChangesAsync();
                return new Response<Product> { Result = product, IsSuccessful = add.IsSuccessful, Message = add.Message };
            }
        }

        public async Task<IResponse<Product>> FindWithAssetsAsync(int id)
        {
            var product = await _productRepo.FindAsync(id);
            if (product == null) return new Response<Product> { Message = ServiceMessage.RecordNotExist };

            return new Response<Product> { Result = product, IsSuccessful = true };
        }

        public async Task<IResponse<Product>> UpdateAsync(ProductAddModel model)//string root, Product model, IList<IFormFile> files)
        {
            var product = await _productRepo.FindAsync(model.ProductId);
            if (product == null) return new Response<Product> { Message = ServiceMessage.RecordNotExist };

            product.StoreId = model.StoreId;
            product.Name = model.Name;
            product.Price = model.Price;
            product.DiscountPercent = model.DiscountPercent;
            product.IsActive = model.IsActive;
            product.Description = model.Description;
            product.ProductCategoryId = model.ProductCategoryId;
            _productRepo.Update(product);
            if (model.Files != null && model.Files.Count != 0)
            {
                var getAssets = await _productAssetService.SaveRange(model);
                if (!getAssets.IsSuccessful) return new Response<Product> { Message = getAssets.Message };
                foreach (var asset in getAssets.Result) asset.ProductId = model.ProductId;
                await _appUow.ProductAssetRepo.AddRangeAsync(getAssets.Result);
                var update = await _appUow.ElkSaveChangesAsync();
                if (!update.IsSuccessful) _productAssetService.DeleteRange(getAssets.Result);
                return new Response<Product> { Result = product, IsSuccessful = update.IsSuccessful, Message = update.Message };
            }
            else
            {
                var update = await _appUow.ElkSaveChangesAsync();
                return new Response<Product> { Result = product, IsSuccessful = update.IsSuccessful, Message = update.Message };
            }
        }

        public async Task<IResponse<bool>> DeleteAsync(string baseDomain, string root, int id)
        {
            var product = await _productRepo.FindAsync(id);
            var urls = _appUow.ProductAssetRepo.Get(x => new { x.FileUrl, x.CdnFileUrl }, x => x.ProductId == id, o => o.OrderBy(x => x.ProductId)).Select(x => (x.FileUrl, x.CdnFileUrl));
            _productRepo.Delete(product);
            var delete = await _appUow.ElkSaveChangesAsync();
            if (delete.IsSuccessful) _productAssetService.DeleteFiles(baseDomain, urls);
            return new Response<bool>
            {
                Message = delete.Message,
                Result = delete.IsSuccessful,
                IsSuccessful = delete.IsSuccessful,
            };
        }

        public PagingListDetails<Product> Get(ProductSearchFilter filter)
        {
            Expression<Func<Product, bool>> conditions = x => !x.IsDeleted;
            if (filter != null)
            {
                if (filter.UserId != null)
                    conditions = conditions.And(x => x.Store.UserId == filter.UserId);
                if (filter.StoreId != null)
                    conditions = conditions.And(x => x.StoreId == filter.StoreId);
                if (!string.IsNullOrWhiteSpace(filter.Name))
                    conditions = conditions.And(x => x.Name.Contains(filter.Name));
            }

            return _productRepo.Get(conditions, filter, x => x.OrderByDescending(i => i.ProductId), new List<Expression<Func<Product, object>>> { x => x.Store });
        }

        public async Task<IResponse<int>> AddRangeAsync(ProductAddRangeModel model)
        {
            var products = model.Posts.Select(x => new Product
            {
                ProductCategoryId = null,
                StoreId = model.StoreId,
                Name = x.Description.Length > 35 ? x.Description.Substring(0, 34) : x.Description,
                Description = x.Description,
                Price = x.Price,
                IsActive = true,
                ProductAssets = x.Assets?.Select(a => new ProductAsset
                {
                    FileType = a.Type,
                    Extention = ".png",
                    Name = "post-image",
                    UniqueId = a.UniqueId,
                    FileUrl = a.FileUrl,
                    ThumbnailUrl = a.ThumbnailUrl
                }).OrderByDescending(x => x.UniqueId).Take(3).ToList()
            }).ToList();
            await _productRepo.AddRangeAsync(products);
            var save = await _appUow.ElkSaveChangesAsync();
            return new Response<int>
            {
                IsSuccessful = save.IsSuccessful,
                Result = save.Result,
                Message = save.Message
            };
        }

        public async Task<IResponse<List<Post>>> GetPosts(string username, int pageNumber)
        {
            try
            {
                using var getPostsHttp = new HttpClient();
                var apiCall = await getPostsHttp.GetAsync($"{_configuration["CustomSettings:Crawler:GetPosts"]}?pageSize=6&username={username}&pageNumber={pageNumber}");
                if (!apiCall.IsSuccessStatusCode) return new Response<List<Post>> { Message = ServiceMessage.Error };
                var getPosts = (await apiCall.Content.ReadAsStringAsync()).DeSerializeJson<Response<List<Post>>>();
                if (!getPosts.IsSuccessful) return new Response<List<Post>> { Message = getPosts.Message };
                return new Response<List<Post>> { IsSuccessful = true, Result = getPosts.Result };
            }
            catch(Exception e)
            {
                FileLoger.Error(e);
                return new Response<List<Post>> { Message = ServiceMessage.GetPostsFailed };

            }

        }
    }
}
