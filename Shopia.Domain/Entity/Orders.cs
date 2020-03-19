using System;
using Elk.Core;
using System.Linq;
using System.Collections.Generic;
using Shopia.Domain.Enum;

namespace Shopia.Domain.Entity
{
    public class Orders
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int PageId { get; set; }

        public int FromAddressId { get; set; }
        public int ToAddressId { get; set; }

        public int DiscountId { get; set; }
        public int DiscountPrice { get; set; }

        public DateTime InsertDateMi { get; set; }

        public int DeliveryProviderId { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime DoingTime { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DelicerySpan DelicerySpan { get; set; }

        public string UserDescription { get; set; }
        public string OrderDescription { get; set; }
    }
}
