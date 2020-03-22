using System;

namespace Shopia.Domain
{
    public enum PaymentStatus : byte
    {
        Failed = -1,
        Insert = 0,
        Success = 1,
    }
}