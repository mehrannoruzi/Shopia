using Elk.Core;
using Shopia.Domain.Resource;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shopia.Domain
{
    public class ProductSearchFilter : PagingParameter
    {
        public Guid? UserId { get; set; }
        public int? StoreId { get; set; }
        [Display(Name = nameof(Strings.Name), ResourceType = typeof(Strings))]
        public string Name { get; set; }
    }
}
