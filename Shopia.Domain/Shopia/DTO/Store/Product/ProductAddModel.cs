using Microsoft.AspNetCore.Http;
using Shopia.Domain;
using Shopia.Domain.Resource;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [NotMapped]
    public class ProductAddModel//: Product
    {
        public int ProductId { get; set; }

        [Display(Name = nameof(Strings.Store), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public int StoreId { get; set; }


        [Display(Name = nameof(Strings.ProductCategory), ResourceType = typeof(Strings))]
        public int? ProductCategoryId { get; set; }

        [Display(Name = nameof(Strings.Price), ResourceType = typeof(Strings))]
        public int Price { get; set; }

        [Display(Name = nameof(Strings.MaxOrderCount), ResourceType = typeof(Strings))]
        public int MaxOrderCount { get; set; }

        [Display(Name = nameof(Strings.DiscountPercent), ResourceType = typeof(Strings))]
        public float? DiscountPercent { get; set; }

        [Display(Name = nameof(Strings.IsActive), ResourceType = typeof(Strings))]
        public bool IsActive { get; set; }

        [Display(Name = nameof(Strings.Name), ResourceType = typeof(Strings))]
        [MaxLength(35, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(35, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Name { get; set; }

        [Display(Name = nameof(Strings.Description), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(1000, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(1000, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Description { get; set; }

        [NotMapped]
        public IList<IFormFile> Files { get; set; }

        [NotMapped]
        public string Root { get; set; }

        [NotMapped]
        public string BaseDomain { get; set; }

        public List<int> TagIds { get; set; }
    }
}
