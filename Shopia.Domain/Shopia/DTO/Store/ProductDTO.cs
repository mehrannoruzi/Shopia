using System;
using System.Collections.Generic;
namespace Shopia.Domain
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public float? Discount { get; set; }
        public int MaxCount { get; set; }
        public int RealPrice { get { return (int)(Price - (Price * Discount/100)); } }
        public string ImageUrl { get; set; }
        public string Description { get; set; }//200 character
        public List<string> Slides { get; set; }
    }
}
