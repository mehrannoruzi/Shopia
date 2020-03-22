using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    public abstract class PaymentBase : IInsertDateProperties
    {
        public int PaymentGatewayId { get; set; }

        public int Amount { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public DateTime InsertDateMi { get; set; }

        public string InsertDateSh { get; set; }

        public string TransactionId { get; set; }
    }
}