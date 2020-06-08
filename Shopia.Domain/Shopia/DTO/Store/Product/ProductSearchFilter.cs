using Elk.Core;
using Shopia.Domain.Resource;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shopia.Domain
{
    public class ProductSearchFilter : PagingParameter
    {
        [Display(Name = nameof(Strings.User), ResourceType = typeof(Strings))]
        public Guid? UserId { get; set; }
        [Display(Name = nameof(Strings.Store), ResourceType = typeof(Strings))]
        public int? StoreId { get; set; }
        [Display(Name = nameof(Strings.Name), ResourceType = typeof(Strings))]
        public string Name { get; set; }
        public ProductFilterCategory Category { get; set; }
    }
}
