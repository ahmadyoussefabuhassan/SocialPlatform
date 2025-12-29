using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialPlatform.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<Users , IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // ----- Post Relationships -----
            builder.Entity<Posts>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Posts)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Posts>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
            // ----- Comment Propertes  -----
            builder.Entity<Comments>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
            // ----- User Relationships -----
            builder.Entity<Users>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.Users)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Users>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.Users)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Users>()
                .Property(u => u.Role)
                 .HasConversion<string>();
            // ----- UserBans Relationships -----
            builder.Entity<UserBans>()
                .HasOne(ub => ub.Users)
                .WithMany(u => u.UserBans)
                .HasForeignKey(ub => ub.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserBans>()
                .HasOne(ub => ub.BannedByAdmin)
                .WithMany() 
                .HasForeignKey(ub => ub.BannedByAdminId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<UserBans>()
                .Property(ub => ub.Id)
                .ValueGeneratedOnAdd();
            // ------ Add RefreshToken Relationships -----
            builder.Entity<RefrashToken>()
                .HasOne(u => u.Users)
                .WithMany(r => r.RefrashTokens)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);


        }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<UserBans> UserBans { get; set; }
        public DbSet<RefrashToken> RefrashTokens { get; set; }
    }
}
