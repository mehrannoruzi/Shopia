using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(ProductTemp), Schema = "Store")]
    public class ProductTemp : IInsertDateProperties, IModifyDateProperties, IIsActiveProperty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductTempId { get; set; }

        public int StoreId { get; set; }

        public int Price { get; set; }

        public int LikeCount { get; set; }

        public bool IsSelected { get; set; }

        public DateTime InsertDateMi { get; set; }

        public string InsertDateSh { get; set; }

        public string Description { get; set; }
    }
}
