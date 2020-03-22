using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(Tag), Schema = "Base")]
    public class Tag : IInsertDateProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagId { get; set; }

        public DateTime InsertDateMi { get; set; }

        public string InsertDateSh { get; set; }

        public string Name { get; set; }
    }
}