using System;
namespace Shopia.Domain.Enum
{
    public enum OrderStatus : byte
    {
        Failed=0,
        WaitCrm,
        WaitPayment,
        InProcess,
        WaitDelivery,
        Successed,
    }
}
