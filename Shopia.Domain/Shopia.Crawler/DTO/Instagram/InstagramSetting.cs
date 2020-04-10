namespace Shopia.Domain
{
    public class InstagramSetting
    {
        public InstagramSetting(string pageUrlPattern, string postUrlPattern, string queryHash,
            int maxCrawledPost, int crawledPostPageSize)
        {
            PageUrlPattern = pageUrlPattern;
            PostUrlPattern = postUrlPattern;
            QueryHash = queryHash;
            MaxCrawledPost = maxCrawledPost;
            CrawledPostPageSize = crawledPostPageSize;
        }

        public int MaxCrawledPost { get; set; }
        public int CrawledPostPageSize { get; set; }
        public string QueryHash { get; set; }
        public string PageUrlPattern { get; set; }
        public string PostUrlPattern { get; set; }
    }
}
