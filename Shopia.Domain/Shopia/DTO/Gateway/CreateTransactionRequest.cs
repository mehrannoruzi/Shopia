﻿namespace Shopia.Domain
{
    public class CreateTransactionRequest
    {
        public int GatewayId { get; set; }
        public string ApiKey { get; set; }
        public int OrderId { get; set; }
        public string Url { get; set; }
        public string CallbackUrl { get; set; }
        public int Amount { get; set; }
        public string MobileNumber { get; set; }
        public int Description { get; set; }

    }
}
