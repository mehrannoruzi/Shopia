using System.Collections.Generic;

namespace Shopia.Store.Api
{
    public class OrderDTO
    {
        public AddressDTO Address { get; set; }
        public UserDTO User { get; set; }
        public List<OrderItemDTO> Items { get; set; }
    }
}
