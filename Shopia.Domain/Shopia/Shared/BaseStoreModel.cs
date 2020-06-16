using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    public class BaseStoreModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StoreId { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Display(Name = nameof(Strings.ShopiaUrl), ResourceType = typeof(Strings))]
        [MaxLength(100, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(100, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ShopiaUrl { get; set; }

        [Column(TypeName = "varchar(25)")]
        [Display(Name = nameof(Strings.Username), ResourceType = typeof(Strings))]
        [MaxLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Username { get; set; }

        [Column(TypeName = "varchar(40)")]
        [Display(Name = nameof(Strings.Name), ResourceType = typeof(Strings))]
        [MaxLength(40, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string FullName { get; set; }

        [Column(TypeName = "varchar(1000)")]
        [Display(Name = nameof(Strings.ProfilePictureUrl), ResourceType = typeof(Strings))]
        [MaxLength(1000, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ProfilePictureUrl { get; set; }

        [NotMapped]
        public Address Address { get; set; }
    }
}
