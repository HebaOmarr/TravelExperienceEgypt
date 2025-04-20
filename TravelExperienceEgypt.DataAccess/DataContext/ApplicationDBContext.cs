using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperienceEgypt.DataAccess.Models;

namespace TravelExperienceEgypt.DataAccess.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Govermantate> Govermantates { get; set; }
        public DbSet<ImageUrl> ImageUrls { get; set; }
        public DbSet<Notification> Notications { get; set; }
    

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                 new ApplicationRole { Id = 2, Name = "User", NormalizedName = "USER" });

            modelBuilder.Entity<Comment>()
                .HasKey(c => new { c.UserId, c.PostId });



            modelBuilder.Entity<Comment>()
       .HasOne(c => c.Post)
       .WithMany(p => p.Comments)
       .HasForeignKey(c => c.PostId)
       .OnDelete(DeleteBehavior.NoAction);












            //modelBuilder.Entity<Comment>()
            //    .HasOne(c => c.User)
            //    .WithMany()
            //    .HasForeignKey(c => c.UserId);


            modelBuilder.Entity<Wishlist>()
                .HasKey(w => new { w.PostId, w.UserId });

        


            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.Post)
                .WithMany(p => p.Wishlists)
                .HasForeignKey(w => w.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Wishlist>()
            //    .HasOne(w => w.User)
            //    .WithMany()
            //    .HasForeignKey(w => w.UserId);

            modelBuilder.Entity<Post>()
                .HasIndex(c => new { c.UserId, c.PlaceId })
                .IsUnique();
            modelBuilder.Entity<Post>()
    .Property(p => p.Price)
    .HasColumnType("decimal(10, 2)");
        }

    }
}
