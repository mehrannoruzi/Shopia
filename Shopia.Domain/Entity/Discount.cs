using System;
using System.ComponentModel.DataAnnotations.Schema;
using Shopia.Domain.Enum;

namespace Shopia.Domain
{
    public class Discount
    {
        public int DiscountId { get; set; }

          public DiscountType DiscountType { get; set; }
        
          public int Value { get; set; }

         public int Ceiling { get; set; }

        public bool IsUsed { get; set; }

        /// <summary>
        ///  تعداد مصرف      
        /// </summary>
        public bool IsDisposable { get; set; }

        /// <summary>
        /// تعداد کاربر
        /// </summary>
         public bool ForFirstUser { get; set; }

         public DateTime ValidFromDateMi { get; set; }

         public DateTime ValidToDateMi { get; set; }

        [Column(TypeName = "char")]
           public string ValidFromDateSh { get; set; }

        [Column(TypeName = "char")]
           public string ValidToDateSh { get; set; }

        [Column(TypeName = "varchar")]
           public string Code { get; set; }

    }
}
