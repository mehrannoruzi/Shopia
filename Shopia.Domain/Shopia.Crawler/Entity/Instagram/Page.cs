using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(Page), Schema = "Instagram")]
    public class Page : IInsertDateProperties, IModifyDateProperties, ICrawlerEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PageId { get; set; }

        [Display(Name = nameof(Strings.PostCount), ResourceType = typeof(Strings))]
        public int PostCount { get; set; }

        [Display(Name = nameof(Strings.FolowerCount), ResourceType = typeof(Strings))]
        public int FolowerCount { get; set; }

        [Display(Name = nameof(Strings.FolowingCount), ResourceType = typeof(Strings))]
        public int FolowingCount { get; set; }

        [Display(Name = nameof(Strings.IsPrivate), ResourceType = typeof(Strings))]
        public bool IsPrivate { get; set; }

        [Display(Name = nameof(Strings.IsBlocked), ResourceType = typeof(Strings))]
        public bool IsBlocked { get; set; }

        [Display(Name = nameof(Strings.IsVerified), ResourceType = typeof(Strings))]
        public bool IsVerified { get; set; }

        [Display(Name = nameof(Strings.IsBusinessAccount), ResourceType = typeof(Strings))]
        public bool IsBusinessAccount { get; set; }

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
        [MaxLength(20, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string UniqueId { get; set; }

        [Column(TypeName = "varchar(25)")]
        [Display(Name = nameof(Strings.Username), ResourceType = typeof(Strings))]
        [MaxLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Username { get; set; }

        [Column(TypeName = "varchar(40)")]
        [Display(Name = nameof(Strings.FullName), ResourceType = typeof(Strings))]
        [MaxLength(40, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string FullName { get; set; }

        [Column(TypeName = "varchar(250)")]
        [Display(Name = nameof(Strings.Bio), ResourceType = typeof(Strings))]
        [MaxLength(250, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Bio { get; set; }

        [Column(TypeName = "varchar(150)")]
        [Display(Name = nameof(Strings.BioUrl), ResourceType = typeof(Strings))]
        [MaxLength(150, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string BioUrl { get; set; }

        [Column(TypeName = "varchar(1000)")]
        [Display(Name = nameof(Strings.ProfilePictureUrl), ResourceType = typeof(Strings))]
        [MaxLength(1000, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ProfilePictureUrl { get; set; }

        public List<Post> Posts { get; set; }
    }
}
