using System;

namespace Shopia.Domain
{
    public enum OrderStatus : int
    {
        Failed = -1,
        
        WaitForCrm = 1,
        WaitForPayment = 2,
        InProcessing = 3,
        WaitForDelivery = 4,
        
        Successed = 10,
    }
}
