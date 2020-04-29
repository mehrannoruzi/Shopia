using System.Collections.Generic;

namespace Shopia.Store.Api
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public byte Discount { get; set; }
        public int MaxCount { get; set; }
        public int RealPrice { get { return Price - (Price * Discount/100); } }
        public string Currency { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }//200 character
        public List<string> Slides { get; set; }
    }
}
