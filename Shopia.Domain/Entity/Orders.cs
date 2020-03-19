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

        public int  PostId { get; set; }
        public DateTime InsertDateMi { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime DoingTime { get; set; }

    }
}
