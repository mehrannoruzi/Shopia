using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(DeliveryProvider), Schema = "Delivery")]
    public class DeliveryProvider : IInsertDateProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeliveryProviderId { get; set; }

        public DateTime InsertDateMi { get; set; }

        public string InsertDateSh { get; set; }

        public string Username { get; set; }
        
        public string Password { get; set; }

        public string Name { get; set; }
        
        public string OrderUrl { get; set; }
        
        public string InqueryUrl { get; set; }
    }
}
