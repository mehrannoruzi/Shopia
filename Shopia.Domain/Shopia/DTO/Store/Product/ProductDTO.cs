using System;
using System.Collections.Generic;
namespace Shopia.Domain
{
    public class ProductDTO : OrderItemDTO
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }//200 character
        public List<string> Slides { get; set; }
    }
}
