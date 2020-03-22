using System;
using Shopia.Domain.Enum;

namespace Shopia.Domain.Entity.Base
{
    public class DeliveryTimeTable
    {
        public int DeliveryTimeTableId { get; set; }

        public DateTime AvailableDate { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsPublicHoliday { get; set; }

        public DeliverySpan DeliverySpan { get; set; }

    }
}
