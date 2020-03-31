using Enum;
using System;
using Properties;
using Gnu.Framework.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(BankAccount), Schema = "Base")]
    public class BankAccount : IInsertDateProperties, IModifyDateProperties, ISoftDeleteProperty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BankAccountId { get; set; }

        [Display(Name = nameof(DisplayName.BankName), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public BankName BankName { get; set; }

        [Display(Name = nameof(DisplayName.IsDeleted), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public bool IsDeleted { get; set; }

        [Display(Name = nameof(DisplayName.InsertDateMi), ResourceType = typeof(DisplayName))]
        public DateTime InsertDateMi { get; set; }

        [Display(Name = nameof(DisplayName.InsertDateMi), ResourceType = typeof(DisplayName))]
        public DateTime ModifyDateMi { get; set; }

        [Column(TypeName = "char")]
        [Display(Name = nameof(DisplayName.InsertDateSh), ResourceType = typeof(DisplayName))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string InsertDateSh { get; set; }

        [Column(TypeName = "char")]
        [Display(Name = nameof(DisplayName.InsertDateSh), ResourceType = typeof(DisplayName))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ModifyDateSh { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public Guid UserId { get; set; }

        [Column(TypeName = "varchar")]
        [Display(Name = nameof(DisplayName.AccountNumber), ResourceType = typeof(DisplayName))]
        [MaxLength(20, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(20, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string AccountNumber { get; set; }

        [Column(TypeName = "varchar")]
        [Display(Name = nameof(DisplayName.CardNumber), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(19, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MinLength(19, ErrorMessageResourceName = nameof(ErrorMessage.MinLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(19, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [RegularExpression(@"^\d{4,4}\-\d{4,4}\-\d{4,4}\-\d{4,4}$", ErrorMessageResourceName = nameof(ErrorMessage.CardNumberFormat), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string CardNumber { get; set; }

        [Column(TypeName = "varchar")]
        [Display(Name = nameof(DisplayName.Shaba), ResourceType = typeof(DisplayName))]
        [MaxLength(24, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(24, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Shaba { get; set; }

        [Column(TypeName = "nvarchar")]
        [Display(Name = nameof(DisplayName.Description), ResourceType = typeof(DisplayName))]
        [MaxLength(200, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(200, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Description { get; set; }
    }
}