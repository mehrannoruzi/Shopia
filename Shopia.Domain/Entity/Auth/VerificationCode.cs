using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{

    [Table(nameof(VerificationCode), Schema = "Auth")]
    public class VerificationCode : IInsertDateProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VerificationCodeId { get; set; }

        [Display(Name = nameof(DisplayName.IsBlock), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public bool IsUsed { get; set; }

        [Display(Name = nameof(DisplayName.InsertDateMi), ResourceType = typeof(DisplayName))]
        public DateTime? UsedDateMi { get; set; }

        [Display(Name = nameof(DisplayName.InsertDateMi), ResourceType = typeof(DisplayName))]
        public DateTime InsertDateMi { get; set; }

        [Column(TypeName = "char")]
        [Display(Name = nameof(DisplayName.InsertDateSh), ResourceType = typeof(DisplayName))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string InsertDateSh { get; set; }

        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public Guid UserId { get; set; }

        [Column(TypeName = "varchar")]
        [Display(Name = nameof(DisplayName.Code), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(36, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(36, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Code { get; set; }
    }
}