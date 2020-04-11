namespace Shopia.Domain
{
    public class InstagramSetting
    {
        public InstagramSetting(string pageUrlPattern, string postUrlPattern, string queryHash,
            int maxCrawledPost, int crawledPostPageSize, string updatePostCronPattern)
        {
            PageUrlPattern = pageUrlPattern;
            PostUrlPattern = postUrlPattern;
            QueryHash = queryHash;
            MaxCrawledPost = maxCrawledPost;
            CrawledPostPageSize = crawledPostPageSize;
            UpdatePostCronPattern = updatePostCronPattern;
        }

        public int MaxCrawledPost { get; set; }
        public int CrawledPostPageSize { get; set; }
        public string QueryHash { get; set; }
        public string PageUrlPattern { get; set; }
        public string PostUrlPattern { get; set; }
        public string UpdatePostCronPattern { get; set; }
    }
}
