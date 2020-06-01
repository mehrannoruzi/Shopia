using System;
using System.Collections.Generic;

namespace Shopia.Domain
{
    public class ProductAddRangeModel
    {
        public int StoreId { get; set; }

        public IList<PostModel> Posts { get; set; }
    }
}
