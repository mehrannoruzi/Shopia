using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(Product), Schema = "Store")]
    public class Product : IInsertDateProperties, IModifyDateProperties, IIsActiveProperty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        public int StoreId { get; set; }

        public int? ProductCategoryId { get; set; }

        public int Price { get; set; }

        public float DiscountPercent { get; set; }

        public bool IsActive { get; set; }

        public DateTime InsertDateMi { get; set; }

        public DateTime ModifyDateMi { get; set; }

        public string InsertDateSh { get; set; }

        public string ModifyDateSh { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
