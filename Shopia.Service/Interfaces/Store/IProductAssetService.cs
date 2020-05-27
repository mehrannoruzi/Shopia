using Elk.Core;
using Microsoft.AspNetCore.Http;
using Shopia.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public interface IProductAssetService
    {
        Task<IResponse<string>> DeleteAsync(int id);
        IResponse<string> DeleteRange(IList<ProductAsset> assets);
        IResponse<string> DeleteRange(int productId);
        Task<IResponse<IList<ProductAsset>>> SaveRange(ProductAddModel model);
        IResponse<string> DeleteFiles(string baseDomain, IEnumerable<(string fileUrl, string cdnUrl)> urls);
    }
}