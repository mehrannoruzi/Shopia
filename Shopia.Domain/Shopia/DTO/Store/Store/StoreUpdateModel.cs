using Shopia.Domain.Resource;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shopia.Domain
{
    public class StoreUpdateModel : BaseStoreModel
    {
        //public int StoreId { get; set; }

        //public string ProfilePictureUrl { get; set; }
        //[Display(Name = nameof(Strings.ShopiaUrl), ResourceType = typeof(Strings))]
        //public string ShopiaUrl { get; set; }

        //[Display(Name = nameof(Strings.Username), ResourceType = typeof(Strings))]
        //[MaxLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        //public string Username { get; set; }

        //[Display(Name = nameof(Strings.FullName), ResourceType = typeof(Strings))]
        //[MaxLength(40, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        //public string FullName { get; set; }
        [Display(Name = nameof(Strings.IsActive), ResourceType = typeof(Strings))]
        public bool IsActive { get; set; }
        public string Root { get; set; }
        public IFormFile Logo { get; set; }
        public string BaseDomain { get; set; }
        //[Display(Name = nameof(Strings.Latitude), ResourceType = typeof(Strings))]
        //[Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        //public double? Latitude { get; set; } = 35.699858;

        //[Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        //[Display(Name = nameof(Strings.Longitude), ResourceType = typeof(Strings))]
        //public double? Longitude { get; set; } = 51.337848;

        //[Display(Name = nameof(Strings.Address), ResourceType = typeof(Strings))]
        //[Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        //[MaxLength(250, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        //[StringLength(250, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        //public string AddressDetails { get; set; }

    }
}
