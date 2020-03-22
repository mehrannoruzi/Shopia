using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(Address), Schema = "Base")]
    public class Address : IInsertDateProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }

        public Guid UserId { get; set; }

        public AddressType AddressType { get; set; }

        public bool IsDefault { get; set; }

        public double Latetude { get; set; }

        public double Longetude { get; set; }

        public DateTime InsertDateMi { get; set; }

        public string InsertDateSh { get; set; }

        public string Name { get; set; }

        public string Details { get; set; }

    }
}
