using System;
using Elk.Core;
using System.Linq;
using System.Collections.Generic;

namespace Shopia.Domain.Entity
{
    public class OrderItems
    {
        public int OrderItemsId { get; set; }
        public int PostId { get; set; }

        public int Count { get; set; }
        public int Price { get; set; }
        public int TotalPrice { get; set; }



    }
}
