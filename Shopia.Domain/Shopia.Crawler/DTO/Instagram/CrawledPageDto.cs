using System;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    public class CrawledPageDto
    {
        public int PageId { get; set; }
        public int PostCount { get; set; }
        public int FolowerCount { get; set; }
        public int FolowingCount { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsVerified { get; set; }
        public bool IsBusinessAccount { get; set; }
        public DateTime LastCrawlDate { get; set; }

        [Column(TypeName = "varchar(20)")]
        [MaxLength(20, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string UniqueId { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Username { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(40, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string FullName { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(250, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Bio { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(150, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string BioUrl { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(1000, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ProfilePictureUrl { get; set; }
    }
}
