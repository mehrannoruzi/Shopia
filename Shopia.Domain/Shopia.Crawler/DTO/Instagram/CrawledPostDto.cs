using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    public class CrawledPostDto
    {
        public int PageId { get; set; }
        public int ViewCount { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }

        public FileType Type { get; set; }

        public bool IsAlbum { get; set; }

        public List<CrawledPostDto> Items { get; set; }

        public DateTime CreateDateMi { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(9, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Dimension { get; set; }

        [Column(TypeName = "char")]
        [MaxLength(18, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string UniqueId { get; set; }

        [Column(TypeName = "nvarchar")]
        [MaxLength(2500, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Description { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(1000, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string FileUrl { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(1000, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ThumbnailUrl { get; set; }
    }
}
