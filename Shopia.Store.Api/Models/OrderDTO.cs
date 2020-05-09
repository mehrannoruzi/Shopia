using Shopia.Domain;
using System.Collections.Generic;

namespace Shopia.Store.Api
{
    public class OrderDTO
    {
        public string OrderId { get; set; }
        public int DeliveryId { get; set; }       
        public AddressDTO Address { get; set; }
        public UserDTO User { get; set; }
        public string Reciever { get; set; }
        public string RecieverMobileNumber { get; set; }
        public List<OrderItemDTO> Items { get; set; }
    }
}
