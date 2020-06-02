using System;
using System.ComponentModel;

namespace Shopia.Domain
{
    public enum PaymentStatus : int
    {
        [Description("ناموفق")]
        Failed = -1,
        [Description("ثبت شده")]
        Insert = 0,
        [Description("موفق")]
        Success = 1,
    }
}