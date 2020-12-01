using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlogAdmin.Models
{
    public class BlogDBContext : DbContext
    {
        public BlogDBContext()
        {

        }

        public BlogDBContext(DbContextOptions options) : base(options)
        {

        }


        public virtual DbSet<Category> Category { get; set; }

        public virtual DbSet<post> post { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-UAT2GRF\\SQLEXPRESS;Database=BlogueDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.categoryID).HasColumnName("categoryID");
                entity.Property(e => e.title).HasColumnName("title");
            });
            modelBuilder.Entity<post>(entity =>
            {
                entity.Property(e => e.title).HasColumnName("title");
                entity.Property(e => e.postID).HasColumnName("postID");
                entity.Property(e => e.publicationDate).HasColumnName("publicationDate");
                entity.Property(e => e.publicationDate).HasColumnName("content");

            });

        }

    }
}
