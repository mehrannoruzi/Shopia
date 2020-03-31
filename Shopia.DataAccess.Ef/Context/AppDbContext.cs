using Elk.Core;
using Shopia.Domain;
using Elk.EntityFrameworkCore;
using Elk.EntityFrameworkCore.Tools;
using Microsoft.EntityFrameworkCore;

namespace Shopia.DataAccess.Ef
{
    public class AppDbContext : ElkDbContext
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasIndex(x => x.MobileNumber).HasName("IX_MobileNumber").IsUnique();
            builder.Entity<Tag>().HasIndex(x => x.Title).HasName("IX_Title").IsUnique();

            builder.OverrideDeleteBehavior();
            builder.RegisterAllEntities<IEntity>(typeof(User).Assembly);
        }
    }
}