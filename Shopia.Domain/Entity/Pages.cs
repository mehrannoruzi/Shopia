using System;

namespace Shopia.Domain.Entity
{
    public class Pages
    {
        public int PagesId { get; set; }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public int FollowerCount { get; set; }
        public int FollowingCount { get; set; }
        /// <summary>
        /// last date of crawl
        /// </summary>
        public DateTime LastUpdate { get; set; }
        public string PictureUrl { get; set; }

        public string CrawlSchedulePattern { get; set; }
        public bool IsActive { get; set; }
    }
}
