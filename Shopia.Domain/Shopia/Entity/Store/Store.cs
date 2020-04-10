using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(Store), Schema = "Store")]
    public class Store : IInsertDateProperties, IModifyDateProperties, ISoftDeleteProperty, IIsActiveProperty, IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StoreId { get; set; }

        [ForeignKey(nameof(UserId))]
        [Display(Name = nameof(Strings.User), ResourceType = typeof(Strings))]
        public User User { get; set; }
        [Display(Name = nameof(Strings.User), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public Guid UserId { get; set; }

        [Display(Name = nameof(Strings.Address), ResourceType = typeof(Strings))]
        public int? AddressId { get; set; }

        [Display(Name = nameof(Strings.StoreType), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public StoreType StoreType { get; set; }

        [Display(Name = nameof(Strings.StoreStatus), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public StoreStatus StoreStatus { get; set; }

        [Display(Name = nameof(Strings.PreparationDay), ResourceType = typeof(Strings))]
        public int? PreparationDay { get; set; }

        [Display(Name = nameof(Strings.ProductCount), ResourceType = typeof(Strings))]
        public int? ProductCount { get; set; }

        [Display(Name = nameof(Strings.PostCount), ResourceType = typeof(Strings))]
        public int? PostCount { get; set; }

        [Display(Name = nameof(Strings.FolowerCount), ResourceType = typeof(Strings))]
        public int? FolowerCount { get; set; }

        [Display(Name = nameof(Strings.FolowingCount), ResourceType = typeof(Strings))]
        public int? FolowingCount { get; set; }

        [Display(Name = nameof(Strings.IsActive), ResourceType = typeof(Strings))]
        public bool IsActive { get; set; }

        [Display(Name = nameof(Strings.IsDeleted), ResourceType = typeof(Strings))]
        public bool IsDeleted { get; set; }

        [Display(Name = nameof(Strings.LastCrawlTime), ResourceType = typeof(Strings))]
        public DateTime? LastCrawlTime { get; set; }

        [Display(Name = nameof(Strings.InsertDate), ResourceType = typeof(Strings))]
        public DateTime InsertDateMi { get; set; }

        [Display(Name = nameof(Strings.ModifyDate), ResourceType = typeof(Strings))]
        public DateTime ModifyDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(Strings.InsertDate), ResourceType = typeof(Strings))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string InsertDateSh { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(Strings.ModifyDate), ResourceType = typeof(Strings))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ModifyDateSh { get; set; }

        [Column(TypeName = "varchar(20)")]
        [Display(Name = nameof(Strings.UniqueId), ResourceType = typeof(Strings))]
        [MaxLength(20, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string UniqueId { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Display(Name = nameof(Strings.ShopiaUrl), ResourceType = typeof(Strings))]
        [MaxLength(100, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(100, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ShopiaUrl { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Display(Name = nameof(Strings.TelegramUrl), ResourceType = typeof(Strings))]
        [MaxLength(100, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(100, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string TelegramUrl { get; set; }

        [Column(TypeName = "varchar(150)")]
        [Display(Name = nameof(Strings.WhatsAppUrl), ResourceType = typeof(Strings))]
        [MaxLength(150, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(150, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string WhatsAppUrl { get; set; }


        public List<Product> Products { get; set; }
    }
}
