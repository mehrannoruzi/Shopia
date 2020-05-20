using System;
using System.Collections.Generic;

namespace Shopia.Domain
{
    public class ProductAddModel
    {
        public int StoreId { get; set; }

        public IList<PostModel> Posts { get; set; }
    }
}
