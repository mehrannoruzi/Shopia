﻿using System;
using System.Collections.Generic;

namespace Shopia.Domain
{
    public class OrderDTO
    {
        public Guid? BasketId { get; set; }
        public int? OrderId { get; set; }
        public int GatewayId { get; set; }
        public int DeliveryId { get; set; }       
        public AddressDTO Address { get; set; }
        public Guid UserToken { get; set; }
        public string Description { get; set; }
        public string Reciever { get; set; }
        public string RecieverMobileNumber { get; set; }
        public List<OrderItemDTO> Items { get; set; }
    }
}
