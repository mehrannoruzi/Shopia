using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(Order), Schema = "Order")]
    public class Order : IInsertDateProperties, IModifyDateProperties, ISoftDeleteProperty, IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [ForeignKey(nameof(StoreId))]
        [Display(Name = nameof(Strings.Store), ResourceType = typeof(Strings))]
        public Store Store { get; set; }
        [Display(Name = nameof(Strings.Store), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public int StoreId { get; set; }

        [ForeignKey(nameof(UserId))]
        [Display(Name = nameof(Strings.User), ResourceType = typeof(Strings))]
        public User User { get; set; }
        [Display(Name = nameof(Strings.User), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(FromAddressId))]
        [Display(Name = nameof(Strings.StoreAddress), ResourceType = typeof(Strings))]
        public Address FromAddress { get; set; }
        [Display(Name = nameof(Strings.StoreAddress), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public int FromAddressId { get; set; }

        [ForeignKey(nameof(ToAddressId))]
        [Display(Name = nameof(Strings.CustomerAddress), ResourceType = typeof(Strings))]
        public Address ToAddress { get; set; }
        [Display(Name = nameof(Strings.CustomerAddress), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public int ToAddressId { get; set; }

        [Display(Name = nameof(Strings.DeliveryProvider), ResourceType = typeof(Strings))]
        public int DeliveryProviderId { get; set; }

        [Display(Name = nameof(Strings.DeliveryTime), ResourceType = typeof(Strings))]
        public int DeliveryTime { get; set; }

        [Display(Name = nameof(Strings.Discount), ResourceType = typeof(Strings))]
        public int? DiscountId { get; set; }

        [Display(Name = nameof(Strings.DiscountPrice), ResourceType = typeof(Strings))]
        public int DiscountPrice { get; set; }

        [Display(Name = nameof(Strings.TotalPrice), ResourceType = typeof(Strings))]
        public int TotalPrice { get; set; }

        [Display(Name = nameof(Strings.TotalPriceAfterDiscount), ResourceType = typeof(Strings))]
        public int TotalPriceAfterDiscount { get; set; }

        [Display(Name = nameof(Strings.DeliveryType), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public DeliveryType DeliveryType { get; set; }

        [Display(Name = nameof(Strings.OrderStatus), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public OrderStatus OrderStatus { get; set; }

        [Display(Name = nameof(Strings.IsDeleted), ResourceType = typeof(Strings))]
        public bool IsDeleted { get; set; }

        [Display(Name = nameof(Strings.PreparationDate), ResourceType = typeof(Strings))]
        public DateTime? PreparationDate { get; set; }

        [Display(Name = nameof(Strings.InsertDate), ResourceType = typeof(Strings))]
        public DateTime InsertDateMi { get; set; }

        [Display(Name = nameof(Strings.ModifyDate), ResourceType = typeof(Strings))]
        public DateTime ModifyDateMi { get; set; }

        [Column(TypeName = "char")]
        [Display(Name = nameof(Strings.InsertDate), ResourceType = typeof(Strings))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string InsertDateSh { get; set; }

        [Column(TypeName = "char")]
        [Display(Name = nameof(Strings.ModifyDate), ResourceType = typeof(Strings))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ModifyDateSh { get; set; }

        [Column(TypeName = "varchar")]
        [Display(Name = nameof(Strings.DeliverTrackingId), ResourceType = typeof(Strings))]
        [MaxLength(32, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(32, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string DeliverTrackingId { get; set; }

        [Display(Name = nameof(Strings.OrderComment), ResourceType = typeof(Strings))]
        [MaxLength(150, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(150, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string OrderComment { get; set; }

        [Display(Name = nameof(Strings.UserComment), ResourceType = typeof(Strings))]
        [MaxLength(150, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(150, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string UserComment { get; set; }
    }
}
