using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(Order), Schema = "Order")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public Guid UserId { get; set; }
        public int PageId { get; set; }

        public int FromAddressId { get; set; }
        public int ToAddressId { get; set; }

        public int DiscountId { get; set; }
        public int DiscountPrice { get; set; }

        public DateTime InsertDateMi { get; set; }

        public int DeliveryProviderId { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime DoingTime { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DelicerySpan DelicerySpan { get; set; }

        public string UserDescription { get; set; }
        public string OrderDescription { get; set; }
    }
}
