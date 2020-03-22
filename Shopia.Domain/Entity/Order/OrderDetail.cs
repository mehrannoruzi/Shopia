using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(OrderDetail), Schema = "Order")]
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailId { get; set; }
        public int PostId { get; set; }

        public int Count { get; set; }
        public int Price { get; set; }
        public int TotalPrice { get; set; }



    }
}
