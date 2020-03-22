using System;
namespace Shopia.Domain.Entity.Base
{
    public class DeliveryTimeTable
    {
        public int DeliveryTimeTableId { get; set; }

        public DeliverySpan DeliverySpan { get; set; }
    }
}
