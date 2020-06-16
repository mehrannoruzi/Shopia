using Shopia.Domain.Resource;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shopia.Domain
{
    public class StoreAdminUpdateModel : BaseStoreModel
    {
        [Display(Name = nameof(Strings.StoreStatus), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public StoreStatus StoreStatus { get; set; }
        [Display(Name = nameof(Strings.PreparationDay), ResourceType = typeof(Strings))]
        public int? PreparationDay { get; set; }
        [Display(Name = nameof(Strings.ProductCount), ResourceType = typeof(Strings))]
        public int? ProductCount { get; set; }
        [Display(Name = nameof(Strings.PostCount), ResourceType = typeof(Strings))]
        public int? PostCount { get; set; }
        [Display(Name = nameof(Strings.FolowerCount), ResourceType = typeof(Strings))]
        public int? FolowerCount { get; set; }
        [Display(Name = nameof(Strings.FolowingCount), ResourceType = typeof(Strings))]
        public int? FolowingCount { get; set; }
        [Display(Name = nameof(Strings.IsActive), ResourceType = typeof(Strings))]
        public bool IsActive { get; set; }
        [MaxLength(20, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string UniqueId { get; set; }
        public IFormFile Logo { get; set; }
        public string Root { get; set; }
        public string BaseDomain { get; set; }
    }
}
