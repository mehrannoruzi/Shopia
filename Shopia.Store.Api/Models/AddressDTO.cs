namespace Shopia.Store.Api
{
    public class AddressDTO : LocationDTO
    {
        public int? Id { get; set; }
        public string Address { get; set; }
    }
}
