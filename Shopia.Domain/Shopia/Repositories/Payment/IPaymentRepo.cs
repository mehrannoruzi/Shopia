using Elk.Core;

namespace Shopia.Domain
{
    public interface IPaymentRepo : IGenericRepo<Payment>, IScopedInjection
    {
        PaymentModel GetItemsAndCount(PaymentSearchFilter filter);
    }
}
