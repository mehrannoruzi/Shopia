using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(Post), Schema = "Instagram")]
    public class Post : IInsertDateProperties, ICrawlerEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }

        [ForeignKey(nameof(PageId))]
        [Display(Name = nameof(Strings.Page), ResourceType = typeof(Strings))]
        public Page Page { get; set; }
        [Display(Name = nameof(Strings.Page), ResourceType = typeof(Strings))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public int PageId { get; set; }

        [Display(Name = nameof(Strings.ViewCount), ResourceType = typeof(Strings))]
        public int ViewCount { get; set; }

        [Display(Name = nameof(Strings.LikeCount), ResourceType = typeof(Strings))]
        public int LikeCount { get; set; }

        [Display(Name = nameof(Strings.CommentCount), ResourceType = typeof(Strings))]
        public int CommentCount { get; set; }

        [Display(Name = nameof(Strings.IsAlbum), ResourceType = typeof(Strings))]
        public bool IsAlbum { get; set; }

        [Display(Name = nameof(Strings.CreateDate), ResourceType = typeof(Strings))]
        public DateTime CreateDateMi { get; set; }

        [Column(TypeName = "varchar(20)")]
        [MaxLength(20, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string UniqueId { get; set; }

        [Display(Name = nameof(Strings.InsertDate), ResourceType = typeof(Strings))]
        public DateTime InsertDateMi { get; set; }

        [Column(TypeName = "char(10)")]
        [Display(Name = nameof(Strings.InsertDate), ResourceType = typeof(Strings))]
        [MaxLength(10, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string InsertDateSh { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        [Display(Name = nameof(Strings.Description), ResourceType = typeof(Strings))]
        [MaxLength(1000, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        [StringLength(1000, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Description { get; set; }

        [NotMapped]
        public string PostAssets { get; set; }

        [NotMapped]
        public IEnumerable<PostAsset> AssetList { get { return PostAssets.DeSerializeJson<IEnumerable<PostAsset>>(); } }

        public List<PostAsset> Assets { get; set; }
    }
}
