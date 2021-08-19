using Ingeco.Intranet.Data.Models;
using Microsoft.EntityFrameworkCore;
using SmartB1t.Security.WebSecurity.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Data.Contexts
{
    public class WebDataContext : DbContext
    {
        #region Account sets

        /// <summary>
        /// The database set storing <see cref="User"/>s.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// The database set storing <see cref="UserSecrets"/>.
        /// </summary>
        public DbSet<UserSecrets> Secrets { get; set; }

        /// <summary>
        /// The database set storing <see cref="Role"/>s.
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// The database set storing <see cref="UserRole"/>s.
        /// </summary>
        public DbSet<UserRole> UserRoles { get; set; }

        #endregion

        #region Post sets

        /// <summary>
        /// The database set storing the <see cref="Category"/> recrods.
        /// </summary>
        public DbSet<Category> PostCategories { get; set; }

        /// <summary>
        /// The database set storing the <see cref="WebMedia"/> records.
        /// </summary>
        public DbSet<WebMedia> Media { get; set; }

        /// <summary>
        /// The database set storing the <see cref="Post"/> records.
        /// </summary>
        public DbSet<Post> Posts { get; set; }

        /// <summary>
        /// The database set storing the <see cref="Comment"/> records.
        /// </summary>
        public DbSet<Comment> Comments { get; set; }

        /// <summary>
        /// The database set storing the <see cref="VisitRecord"/> records.
        /// </summary>
        public DbSet<VisitRecord> VisitRecords { get; set; }

        #endregion

        public WebDataContext(DbContextOptions<WebDataContext> options) : base (options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Account modeling

            modelBuilder.Entity<User>().HasKey(u => u.Id);

            modelBuilder.Entity<UserSecrets>()
                        .HasOne(us => us.User)
                        .WithOne(u => u.Secrets)
                        .HasForeignKey<UserSecrets>(us => us.UserId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>()
                        .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                        .HasOne(ur => ur.User)
                        .WithMany(u => u.Roles)
                        .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                        .HasOne(ur => ur.Role)
                        .WithMany(r => r.RoleUsers)
                        .HasForeignKey(ur => ur.RoleId);

            #endregion

            #region Posts modeling

            modelBuilder.Entity<Category>()
                        .HasMany(c => c.Posts)
                        .WithOne(p => p.Category)
                        .HasForeignKey(p => p.CategoryId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                        .HasMany(p => p.Media)
                        .WithOne(m => m.Post)
                        .HasForeignKey(m => m.PostId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                        .HasMany(p => p.Comments)
                        .WithOne(c => c.Post)
                        .HasForeignKey(c => c.PostId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VisitRecord>()
                        .HasOne(r => r.Post)
                        .WithMany(p => p.Visits)
                        .HasForeignKey(r => r.PostId)
                        .OnDelete(DeleteBehavior.Cascade);

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}