using System;

namespace Shopia.Domain
{
    public enum StoreStatus : byte
    {
        Register = 1,
        RequestForCrowle = 2,
        AddProduct = 3,
        RequestForPayment = 4,
        AddPayment = 5
    }
}
