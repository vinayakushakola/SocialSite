using Microsoft.EntityFrameworkCore;
using SocialSiteCommonLayer.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialSiteRepositoryLayer.ApplicationContext
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Users> Users { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasIndex(user => user.Email)
                .IsUnique();
        }
    }
}
