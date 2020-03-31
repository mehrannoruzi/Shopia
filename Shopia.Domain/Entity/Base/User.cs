using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(User), Schema = "Base")]
    public class User : IInsertDateProperties, IAuthEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        //[Display(Name = nameof(Strings.MobileNumber), ResourceType = typeof(Strings))]
        public long MobileNumber { get; set; }

        //[Display(Name = nameof(Strings.Enabled), ResourceType = typeof(Strings))]
        //[Required(ErrorMessageResourceName = nameof(Strings.Required), ErrorMessageResourceType = typeof(Strings))]
        public bool IsActive { get; set; }

        public UserStatus UserStatus { get; set; }
        
        //[Display(Name = nameof(Strings.NewPassword), ResourceType = typeof(Strings))]
        public bool IsRecoveredPassword { get; set; }

        [Display(Name = nameof(Strings.InsertDate), ResourceType = typeof(Strings))]
        public DateTime InsertDateMi { get; set; }

        //[Display(Name = nameof(Strings.LastLoginDate), ResourceType = typeof(Strings))]
        public DateTime? LastLoginDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(Strings.InsertDate), ResourceType = typeof(Strings))]
        //[Required(ErrorMessageResourceName = nameof(Strings.Required), ErrorMessageResourceType = typeof(Strings))]
        //[PersianDate(ErrorMessageResourceName = nameof(Strings.PersianDate), ErrorMessageResourceType = typeof(Strings))]
        //[MaxLength(10, ErrorMessageResourceName = nameof(Strings.MaxLength), ErrorMessageResourceType = typeof(Strings))]
        //[StringLength(10, ErrorMessageResourceName = nameof(Strings.MaxLength), ErrorMessageResourceType = typeof(Strings))]
        public string InsertDateSh { get; set; }

        [Column(TypeName = "char(10)")]
        //[Display(Name = nameof(Strings.LastLoginDate), ResourceType = typeof(Strings))]
        //[MaxLength(10, ErrorMessageResourceName = nameof(Strings.MaxLength), ErrorMessageResourceType = typeof(Strings))]
        public string LastLoginDateSh { get; set; }

        [Column(TypeName = "nvarchar(60)")]
        //[Display(Name = nameof(Strings.FullName), ResourceType = typeof(Strings))]
        //[Required(ErrorMessageResourceName = nameof(Strings.Required), ErrorMessageResourceType = typeof(Strings))]
        //[MaxLength(60, ErrorMessageResourceName = nameof(Strings.MaxLength), ErrorMessageResourceType = typeof(Strings))]
        //[StringLength(60, ErrorMessageResourceName = nameof(Strings.MaxLength), ErrorMessageResourceType = typeof(Strings))]
        public string FullName { get; set; }

        [Column(TypeName = "varchar(50)")]
        //[Required(ErrorMessageResourceName = nameof(Strings.Required), ErrorMessageResourceType = typeof(Strings))]
        //[Display(Name = nameof(Strings.Email), ResourceType = typeof(Strings))]
        //[EmailAddress(ErrorMessageResourceName = nameof(Strings.WrongEmailFormat), ErrorMessageResourceType = typeof(Strings))]
        //[MaxLength(50, ErrorMessageResourceName = nameof(Strings.MaxLength), ErrorMessageResourceType = typeof(Strings))]
        //[StringLength(50, ErrorMessageResourceName = nameof(Strings.MaxLength), ErrorMessageResourceType = typeof(Strings))]
        public string Email { get; set; }

        [Column(TypeName = "char(36)")]
        [DataType(DataType.Password)]
        //[Display(Name = nameof(Strings.Password), ResourceType = typeof(Strings))]
        //[Required(ErrorMessageResourceName = nameof(Strings.Required), ErrorMessageResourceType = typeof(Strings))]
        //[MaxLength(45, ErrorMessageResourceName = nameof(Strings.MaxLength), ErrorMessageResourceType = typeof(Strings))]
        //[StringLength(45, ErrorMessageResourceName = nameof(Strings.MaxLength), ErrorMessageResourceType = typeof(Strings))]
        public string Password { get; set; }
    }
}

