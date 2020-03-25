using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(Discount), Schema = "Order")]
    public class Discount : IInsertDateProperties, IModifyDateProperties, IIsActiveProperty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiscountId { get; set; }

        public int StoreId { get; set; }
        public float Percent { get; set; }
        public bool IsActive { get; set; }

        public DateTime InsertDateMi { get; set; }
        public DateTime ModifyDateMi { get; set; }
        public string InsertDateSh { get; set; }
        public string ModifyDateSh { get; set; }
    }
}
