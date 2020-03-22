using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(ProductCategory), Schema = "Base")]
    public class ProductCategory : IInsertDateProperties, IModifyDateProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductCategoryId { get; set; }

        public int ParentId { get; set; }

        public DateTime InsertDateMi { get; set; }

        public DateTime ModifyDateMi { get; set; }

        public string InsertDateSh { get; set; }

        public string ModifyDateSh { get; set; }

        public string Name { get; set; }

    }
}
