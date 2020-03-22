using System;

namespace Shopia.Domain
{
    public enum PaymentStatus : int
    {
        Failed = -1,
        Insert = 0,
        Success = 1,
    }
}