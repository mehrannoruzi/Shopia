using Shopia.Dashboard.Resources;
using System.ComponentModel.DataAnnotations;

namespace Shopia.Dashboard
{

    public class SignInModel
    {
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessageResourceName = nameof(Strings.WrongEmailFormat), ErrorMessageResourceType = typeof(Strings))]
        [Display(Name = nameof(Strings.MobileNumber), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(Strings.Required), ErrorMessageResourceType = typeof(Strings), AllowEmptyStrings = false)]
        public long Mobilenumber { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(15, ErrorMessageResourceName = nameof(Strings.MaxLength), ErrorMessageResourceType = typeof(Strings))]
        [MinLength(5, ErrorMessageResourceName = nameof(Strings.MinLength), ErrorMessageResourceType = typeof(Strings))]
        [StringLength(15, MinimumLength = 5, ErrorMessageResourceName = nameof(Strings.Min5MaxLength), ErrorMessageResourceType = typeof(Strings))]
        [Display(Name = nameof(Password), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(Strings.Required), ErrorMessageResourceType = typeof(Strings), AllowEmptyStrings = false)]
        public string Password { get; set; }

        [Display(Name = nameof(Strings.RememberMe), ResourceType = typeof(Strings))]
        public bool RememberMe { get; set; }
    }
}