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
        public int StoreId { get; set; }

        public int FromAddressId { get; set; }
        public int ToAddressId { get; set; }
        public int DeliveryProviderId { get; set; }
        public int DeliveryTimeTableId { get; set; }
        public int? DiscountId { get; set; }
        public int DiscountPrice { get; set; }

        public int TotalPrice { get; set; }
        public int TotalPriceAfterDiscount { get; set; }

        public DeliveryType DeliveryType { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime? PreparationDate { get; set; }

        public DateTime InsertDateMi { get; set; }
        public string InsertDateSh { get; set; }

        public string DeliverTrackingId { get; set; }
        public string OrderComment { get; set; }
        public string UserDescription { get; set; }
    }
}
