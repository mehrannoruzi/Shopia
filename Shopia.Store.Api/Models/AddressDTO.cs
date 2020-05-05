namespace Shopia.Store.Api
{
    public class AddressDTO
    {
        public int? Id { get; set; }
        public string Address { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
