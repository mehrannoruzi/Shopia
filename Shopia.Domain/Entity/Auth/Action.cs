using Elk.Core;
using Shopia.Domain.Resource;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(Action), Schema = "Auth")]
    public class Action : IAuthEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActionId { get; set; }

        [Display(Name = nameof(Strings.Parent), ResourceType = typeof(Strings))]
        [ForeignKey(nameof(ParentId))]
        public Action Parent { get; set; }
        [Display(Name = nameof(Strings.Parent), ResourceType = typeof(Strings))]
        public int? ParentId { get; set; }

        [Display(Name = nameof(Strings.OrderPriority), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public byte OrderPriority { get; set; }

        [Display(Name = nameof(Strings.ShowInMenu), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public bool ShowInMenu { get; set; }

        [Column(TypeName = "varchar(25)")]
        [Display(Name = nameof(Strings.ControllerName), ResourceType = typeof(Strings))]
        [MaxLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ControllerName { get; set; }

        [Column(TypeName = "varchar(25)")]
        [Display(Name = nameof(Strings.ActionName), ResourceType = typeof(Strings))]
        [MaxLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ActionName { get; set; }

        [Display(Name = nameof(Strings.Name), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(55, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(55, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Name { get; set; }

        [Column(TypeName = "varchar(40)")]
        [Display(Name = nameof(Strings.Icon), ResourceType = typeof(Strings))]
        [MaxLength(40, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(40, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Icon { get; set; }

        [NotMapped]
        [Display(Name = nameof(Strings.Path), ResourceType = typeof(Strings))]
        public string Path { get { return $"/{ControllerName}/{ActionName}"; } }

        public virtual ICollection<ActionInRole> ActionInRoles { get; set; }
    }
}
