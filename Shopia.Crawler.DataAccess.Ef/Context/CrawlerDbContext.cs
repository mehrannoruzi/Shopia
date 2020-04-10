using Shopia.Domain;
using Elk.EntityFrameworkCore;
using Elk.EntityFrameworkCore.Tools;
using Microsoft.EntityFrameworkCore;

namespace Shopia.DataAccess.Ef
{
    public class CrawlerDbContext : ElkDbContext
    {
        public CrawlerDbContext() { }

        public CrawlerDbContext(DbContextOptions<CrawlerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Page>().HasIndex(x => x.Username).HasName("IX_Username").IsUnique();
            builder.Entity<Post>().HasIndex(x => x.UniqueId).HasName("IX_UniqueId").IsUnique();
            builder.Entity<PostAsset>().HasIndex(x => x.UniqueId).HasName("IX_UniqueId").IsUnique();

            builder.OverrideDeleteBehavior();
            builder.RegisterAllEntities<ICrawlerEntity>(typeof(Page).Assembly);
        }
    }
}
