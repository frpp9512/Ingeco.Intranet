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
            
            base.OnModelCreating(modelBuilder);
        }
    }
}