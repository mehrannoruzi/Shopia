namespace Shopia.Domain
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public float? Discount { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public int MaxCount { get; set; }
        public int RealPrice { get { return (int)(Price - (Price * Discount / 100)); } }
    }
}
