﻿using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(EventMapper), Schema = "Notifier")]
    public class EventMapper : IInsertDateProperties, INotifierEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventMapperId { get; set; }

        [Display(Name = nameof(Strings.Type), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public EventType Type { get; set; }

        [Display(Name = nameof(Strings.InsertDate), ResourceType = typeof(Strings))]
        public DateTime InsertDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(Strings.InsertDate), ResourceType = typeof(Strings))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string InsertDateSh { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Display(Name = nameof(Strings.Receiver), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        [MaxLength(100, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(100, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string NotifyStrategy { get; set; }
    }
}