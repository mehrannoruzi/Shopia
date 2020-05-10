﻿using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(User), Schema = "Base")]
    public class User : IInsertDateProperties, IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        [Display(Name = nameof(Strings.MobileNumber), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public long MobileNumber { get; set; }

        [Display(Name = nameof(Strings.Status), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public UserStatus UserStatus { get; set; }

        [Display(Name = nameof(Strings.IsActive), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public bool IsActive { get; set; }

        [Display(Name = nameof(Strings.IsRecoveredPassword), ResourceType = typeof(Strings))]
        public bool IsRecoveredPassword { get; set; }

        [Display(Name = nameof(Strings.InsertDate), ResourceType = typeof(Strings))]
        public DateTime InsertDateMi { get; set; }

        [Display(Name = nameof(Strings.LastLoginDate), ResourceType = typeof(Strings))]
        public DateTime? LastLoginDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(Strings.InsertDate), ResourceType = typeof(Strings))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string InsertDateSh { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(Strings.LastLoginDate), ResourceType = typeof(Strings))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string LastLoginDateSh { get; set; }

        [Column(TypeName = "char(25)")]
        [DataType(DataType.Password)]
        [Display(Name = nameof(Strings.Password), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Password { get; set; }

        [Display(Name = nameof(Strings.FullName), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(50, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(50, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string FullName { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Display(Name = nameof(Strings.Email), ResourceType = typeof(Strings))]
        //[Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [EmailAddress(ErrorMessageResourceName = nameof(ErrorMessage.WrongEmailFormat), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(50, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(50, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Email { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Display(Name = nameof(Strings.TelegramId), ResourceType = typeof(Strings))]
        //[Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(50, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(50, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string TelegramId { get; set; }


        public List<Address> Addresses { get; set; }
        public List<BankAccount> bankAccounts { get; set; }
        public List<UserAttachment> UserAttachments { get; set; }
        public List<Order> Orders { get; set; }
        public List<Store> Stores { get; set; }
    }
}

