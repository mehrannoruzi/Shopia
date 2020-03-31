using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(PaymentGateway), Schema = "Payment")]
    public class PaymentGateway 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentGatewayId { get; set; }

        public bool IsActive { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string PostBackUrl { get; set; }

        public string MerchantId { get; set; }

        public string Username { get; set; }
        
        public string Password { get; set; }
    }
}
