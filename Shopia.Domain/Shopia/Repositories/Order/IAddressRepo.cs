using Elk.Core;

namespace Shopia.Domain
{
    public interface IOrderRepo : IGenericRepo<Order>, IScopedInjection
    {
    }
}
