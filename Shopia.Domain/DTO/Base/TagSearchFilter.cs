using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;

namespace Shopia.Domain
{
    public class TagSearchFilter : PagingParameter
    {
        [Display(Name = nameof(Strings.Title), ResourceType = typeof(Strings))]
        public string TitleF { get; set; }
    }
}
