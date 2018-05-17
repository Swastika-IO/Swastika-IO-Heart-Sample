using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSwastikaHeart.Models;

namespace DemoSwastikaHeart.Models
{
    public class DemoContext: DbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => new { e.Id });

                entity.ToTable("blog_post");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.Title);

                entity.Property(e => e.Content);

                entity.Property(e => e.Excerpt);

                entity.Property(e => e.CreatedDateUTC);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => new { e.Id });

                entity.ToTable("blog_comment");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.PostId);

                entity.Property(e => e.Author);

                entity.Property(e => e.Content);

                entity.Property(e => e.CreatedDateUTC);
            });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=blogging.db");
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=demo-heart.db;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        public DbSet<DemoSwastikaHeart.Models.Post> Post { get; set; }
        public DbSet<DemoSwastikaHeart.Models.Comment> Comment { get; set; }
    }
}
