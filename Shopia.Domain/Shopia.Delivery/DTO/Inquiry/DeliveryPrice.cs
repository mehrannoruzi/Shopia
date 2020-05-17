namespace Shopia.Domain
{
    public class DeliveryPrice
    {
        public int Price { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int DeliveryProviderId { get; set; }
        public DeliveryType DeliveryType { get; set; }
    }
}
