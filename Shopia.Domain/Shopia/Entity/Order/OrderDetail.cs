using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(OrderDetail), Schema = "Order")]
    public class OrderDetail : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailId { get; set; }

        [ForeignKey(nameof(OrderId))]
        [Display(Name = nameof(Strings.Order), ResourceType = typeof(Strings))]
        public Order Order { get; set; }
        [Display(Name = nameof(Strings.Order), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public int OrderId { get; set; }

        [ForeignKey(nameof(ProductId))]
        [Display(Name = nameof(Strings.Product), ResourceType = typeof(Strings))]
        public Product Product { get; set; }
        [Display(Name = nameof(Strings.Product), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public int ProductId { get; set; }

        [Display(Name = nameof(Strings.Count), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public int Count { get; set; }

        [Display(Name = nameof(Strings.Price), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public int Price { get; set; }

        [Display(Name = nameof(Strings.Discount), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public int DiscountPrice { get; set; }

        [Display(Name = nameof(Strings.TotalPrice), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public int TotalPrice { get; set; }

        [NotMapped]
        public float? DiscountPercent { get; set; }
    }
}
