﻿using System;
using System.ComponentModel;

namespace Shopia.Domain.Enum
{
    public enum DiscountType : byte
    {
        [Description("مقداری")]
        Value = 1,

        [Description("درصدی")]
        Percentage = 2,

        [Description("درصدی با سقف")]
        PercentageWithCeiling = 3
    }
}
