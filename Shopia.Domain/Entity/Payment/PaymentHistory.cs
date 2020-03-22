using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(PaymentHistory), Schema = "Payment")]
    public class PaymentHistory : PaymentBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentHistoryId { get; set; }

        public int PaymentId { get; set; }

    }
}