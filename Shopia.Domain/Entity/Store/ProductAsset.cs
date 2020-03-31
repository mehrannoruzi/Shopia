using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(ProductAsset), Schema = "Store")]
    public class ProductAsset : IInsertDateProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductAssetId { get; set; }

        public int ProductId { get; set; }

        public FileType FileType { get; set; }

        public string Extention { get; set; }
        
        public DateTime InsertDateMi { get; set; }

        public string InsertDateSh { get; set; }
        
        public string Name { get; set; }
        
        public string Url { get; set; }
    }
}