﻿using Shopia.Domain.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shopia.Domain
{
    public class DeliveryDetail
    {
        [Column("varchar(20)")]
        [Display(Name = nameof(Strings.Identifier), ResourceType = typeof(Strings))]
        [MaxLength(20, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(20, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Id { get; set; }

        [Column("varchar(50)")]
        [Display(Name = nameof(Strings.Token), ResourceType = typeof(Strings))]
        [MaxLength(50, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(50, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Token { get; set; }

        [Display(Name = nameof(Strings.Price), ResourceType = typeof(Strings))]
        public int Price { get; set; }

        [Display(Name = nameof(Strings.PayAtDestination), ResourceType = typeof(Strings))]
        public bool PayAtDestination { get; set; }
    }
}
