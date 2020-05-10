﻿using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(Application), Schema = "Notifier")]
    public class Application : IInsertDateProperties, INotifierEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApplicationId { get; set; }

        [Display(Name = nameof(Strings.IsActive), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public bool IsActive { get; set; }

        [Display(Name = nameof(Strings.InsertDate), ResourceType = typeof(Strings))]
        public DateTime InsertDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(Strings.InsertDate), ResourceType = typeof(Strings))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string InsertDateSh { get; set; }

        [Display(Name = nameof(Strings.Token), ResourceType = typeof(Strings))]
        public Guid Token { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = nameof(Strings.Name), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(100, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(100, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Name { get; set; }


        public List<Notification> Notifications { get; set; }
    }
}