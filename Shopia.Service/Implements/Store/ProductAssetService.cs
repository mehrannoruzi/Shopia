using Elk.Core;
using Microsoft.AspNetCore.Http;
using Shopia.DataAccess.Ef;
using Shopia.Domain;
using Shopia.Service.Resource;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public class ProductAssetService : IProductAssetService
    {
        readonly AppUnitOfWork _appUOW;
        readonly IGenericRepo<ProductAsset> _productAssetRepo;
        public ProductAssetService(AppUnitOfWork appUOW)
        {
            _productAssetRepo = appUOW.ProductAssetRepo;
            _appUOW = appUOW;
        }
        public async Task<IResponse<IList<ProductAsset>>> SaveRange(ProductAddModel model)
        {
            try
            {
                var items = new List<ProductAsset>();
                var pdt = PersianDateTime.Now;
                var dir = $"/Files/{model.StoreId}/{pdt.Year}/{pdt.Month}";
                if (!FileOperation.CreateDirectory(model.Root + dir))
                    return new Response<IList<ProductAsset>> { Message = ServiceMessage.SaveFileFailed };
                foreach (var file in model.Files)
                {
                    var relativePath = $"{dir}/{Guid.NewGuid().ToString().Replace("-", "_")}{Path.GetExtension(file.FileName)}";
                    var physicalPath = (model.Root + relativePath).Replace("/", "\\");
                    items.Add(new ProductAsset
                    {
                        Name = file.FileName,
                        Extention = Path.GetExtension(file.FileName),
                        FileType = FileOperation.GetFileType(file.FileName),
                        FileUrl = model.BaseDomain + relativePath,
                        CdnFileUrl = physicalPath
                    });
                    using (var stream = File.Create(physicalPath))
                        await file.CopyToAsync(stream);
                }

                return new Response<IList<ProductAsset>> { IsSuccessful = true, Result = items };
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                return new Response<IList<ProductAsset>>
                {
                    Message = ServiceMessage.SaveFileFailed
                };
            }

        }

        public IResponse<string> DeleteRange(IList<ProductAsset> assets)
        {
            try
            {
                foreach (var asset in assets)
                {
                    if (File.Exists(asset.CdnFileUrl))
                        File.Delete(asset.CdnFileUrl);
                }
                return new Response<string> { IsSuccessful = true };
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                return new Response<string> { Message = ServiceMessage.Error };
            }

        }

        public IResponse<string> DeleteRange(int productId)
        {
            try
            {
                foreach (var asset in _productAssetRepo.Get(conditions: x => x.ProductId == productId, null))
                {
                    if (File.Exists(asset.CdnFileUrl))
                        File.Delete(asset.CdnFileUrl);
                }
                return new Response<string> { IsSuccessful = true };
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                return new Response<string> { Message = ServiceMessage.Error };
            }

        }

        public async Task<IResponse<string>> DeleteAsync(int id)
        {
            try
            {
                var asset = await _productAssetRepo.FindAsync(id);
                if (asset == null)
                    return new Response<string> { Message = ServiceMessage.RecordNotExist };
                _productAssetRepo.Delete(asset);
                var delete = await _appUOW.ElkSaveChangesAsync();
                if (File.Exists(asset.CdnFileUrl))
                    File.Delete(asset.CdnFileUrl);
                return new Response<string> { IsSuccessful = delete.IsSuccessful, Message = delete.Message };
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                return new Response<string> { Message = ServiceMessage.Error };
            }

        }

        public IResponse<string> DeleteFiles(string baseDomain, IEnumerable<(string fileUrl, string cdnUrl)> urls)
        {
            try
            {
                if (urls == null) return new Response<string> { IsSuccessful = true };
                foreach (var url in urls)
                    if (url.fileUrl.StartsWith(baseDomain))
                        File.Delete(url.cdnUrl);
                return new Response<string> { IsSuccessful = true };
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                return new Response<string>
                {
                    Message = ServiceMessage.Error
                };
            }

        }
    }
}
