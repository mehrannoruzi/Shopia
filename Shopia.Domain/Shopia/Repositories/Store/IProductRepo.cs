using Elk.Core;

namespace Shopia.Domain
{
    public interface IProductRepo : IGenericRepo<Product>, IScopedInjection
    {
    }
}
