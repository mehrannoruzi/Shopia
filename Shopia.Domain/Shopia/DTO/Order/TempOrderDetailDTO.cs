using Shopia.Domain.Resource;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shopia.Domain
{
    public class TempOrderDetailDTO
    {
        [Display(Name = nameof(Strings.InsertDate), ResourceType = typeof(Strings))]
        public string InsertDateSh { get; set; }
        [Display(Name = nameof(Strings.BasketId), ResourceType = typeof(Strings))]
        public Guid BasketId { get; set; }
        [Display(Name = nameof(Strings.TotalPrice), ResourceType = typeof(Strings))]
        public int TotalPrice { get; set; }
    }
}
