using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(UserAttachment), Schema = "Base")]
    public class UserAttachment : IInsertDateProperties, ISoftDeleteProperty, IAuthEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserAttachmentId { get; set; }

        [Display(Name = nameof(DisplayName.FileType), ResourceType = typeof(DisplayName))]
        public FileType FileType { get; set; }

        [Display(Name = nameof(DisplayName.AttachmentType), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public UserAttachmentType UserAttachmentType { get; set; }

        [Display(Name = nameof(DisplayName.Size), ResourceType = typeof(DisplayName))]
        public int Size { get; set; }

        [Display(Name = nameof(DisplayName.IsDeleted), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public bool IsDeleted { get; set; }

        [Display(Name = nameof(DisplayName.InsertDateMi), ResourceType = typeof(DisplayName))]
        public DateTime InsertDateMi { get; set; }

        [Column(TypeName = "char")]
        [Display(Name = nameof(DisplayName.InsertDateSh), ResourceType = typeof(DisplayName))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string InsertDateSh { get; set; }

        //[ForeignKey(nameof(UserId))]
        //public User User { get; set; }
        public Guid UserId { get; set; }

        [Column(TypeName = "varchar")]
        [Display(Name = nameof(DisplayName.Extention), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(5, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(5, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Extention { get; set; }

        [Column(TypeName = "nvarchar")]
        [Display(Name = nameof(DisplayName.Name), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(18, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(18, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Name { get; set; }

        public string Url { get; set; }
    }
}
