using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(ProductTag), Schema = "Store")]
    public class ProductTag : IInsertDateProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductTagId { get; set; }

        public int ProductId { get; set; }

        public int TagId { get; set; }

        public DateTime InsertDateMi { get; set; }

        public string InsertDateSh { get; set; }
    }
}