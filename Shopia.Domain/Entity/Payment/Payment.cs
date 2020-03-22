using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(Payment), Schema = "Payment")]
    public class Payment : PaymentBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }

    }
}