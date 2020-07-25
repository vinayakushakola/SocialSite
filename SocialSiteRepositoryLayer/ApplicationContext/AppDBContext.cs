using Microsoft.EntityFrameworkCore;
using SocialSiteCommonLayer.DBModels;

namespace SocialSiteRepositoryLayer.ApplicationContext
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Users> Users { set; get; }
        public DbSet<Posts> Posts { set; get; }
        public DbSet<Likes> Likes { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasIndex(user => user.Email)
                .IsUnique();
        }
    }
}
