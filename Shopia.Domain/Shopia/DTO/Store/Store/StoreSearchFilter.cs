using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;

namespace Shopia.Domain
{
    public class StoreSearchFilter : PagingParameter
    {
        [Display(Name = nameof(Strings.Name), ResourceType = typeof(Strings))]
        public string Name { get; set; }
    }
}
