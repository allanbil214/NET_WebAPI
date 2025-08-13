using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YoutubeAPI.Models.Entities;

namespace YoutubeAPI.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Video> Videos { get; set; }
        public DbSet<Youtuber> Youtubers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Video>(e =>
            {
                e.ToTable("Videos");
                e.HasKey(v => v.Id);
                e.Property(v => v.Title)
                    .IsRequired()
                    .HasMaxLength(200);
                e.Property(v => v.Description)
                    .IsRequired();
                e.Property(v => v.Url).IsRequired();
                e.Property(v => v.ViewCount)
                    .HasDefaultValue(0);
                e.Property(v => v.LikeCount)
                    .HasDefaultValue(0);
                e.Property(v => v.DislikeCount)
                    .HasDefaultValue(0);
                e.Property(v => v.PublishedAt)
                    .HasDefaultValueSql("datetime('now')");
                e.Property(v => v.CreatedAt)
                    .HasDefaultValueSql("datetime('now')");
                e.Property(v => v.UpdatedAt)
                    .HasDefaultValueSql("datetime('now')");

                e.HasIndex(v => v.Url)
                    .IsUnique();

                e.HasOne(v => v.Youtuber)
                    .WithMany(y => y.Videos)
                    .HasForeignKey(v => v.YoutuberID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Youtuber>(e =>
            {
                e.HasKey(y => y.Id);
                e.Property(y => y.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                e.Property(y => y.ChannelName)
                    .IsRequired()
                    .HasMaxLength(100);
                e.Property(y => y.Email)
                    .IsRequired()
                    .HasMaxLength(100);
                e.Property(y => y.Subscriber)
                    .HasDefaultValue(0);
                e.Property(y => y.CreatedAt)
                    .HasDefaultValueSql("datetime('now')");
                e.Property(y => y.UpdatedAt)
                    .HasDefaultValueSql("datetime('now')");

                e.HasIndex(y => y.Email)
                    .IsUnique();

                e.HasMany(v => v.Videos)
                    .WithOne(y => y.Youtuber)
                    .HasForeignKey(v => v.YoutuberID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ApplicationUser>(e =>
            {
                e.Property(u => u.CreatedAt)
                    .HasDefaultValueSql("datetime('now')");
                e.Property(u => u.UpdatedAt)
                    .HasDefaultValueSql("datetime('now')");
            });

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "a1b2c3d4-e5f6-7890-abcd-123456789abc",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "role-admin-stamp"
                },
                new IdentityRole
                {
                    Id = "b2c3d4e5-f6g7-8901-bcde-234567890def",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "role-user-stamp"
                }
            );

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "c3d4e5f6-g7h8-9012-cdef-345678901ghi",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@youtube.com",
                    NormalizedEmail = "ADMIN@YOUTUBE.COM",
                    EmailConfirmed = true,
                    SecurityStamp = "d4e5f6g7-h8i9-0123-defg-456789012jkl",
                    ConcurrencyStamp = "role-admin-stamp",
                    PasswordHash = "AQAAAAEAACcQAAAAENQjeGx4Pe5dqDKN8rTAMqyjqIV9W2aiP8SsuH3wXaHL9XyeAp/b8sJXwVZCMls4Bg=="
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "a1b2c3d4-e5f6-7890-abcd-123456789abc",
                    UserId = "c3d4e5f6-g7h8-9012-cdef-345678901ghi"
                }
            );

            modelBuilder.Entity<Youtuber>().HasData(
                new Youtuber
                {
                    Id = 1,
                    Name = "penguinz0",
                    ChannelName = "penguinz0",
                    Email = "Cr1TiKaLContact@gmail.com",
                    Subscriber = 17100000
                },
                new Youtuber
                {
                    Id = 2,
                    Name = "Linus Tech Tips",
                    ChannelName = "LinusTechTips",
                    Email = "support@lttstore.com",
                    Subscriber = 16500000
                }
            );

            modelBuilder.Entity<Video>().HasData(
                new Video
                {
                    Id = 1,
                    Title = "This is the Best FPS Right Now But Everyone is Sleeping On It",
                    Description = "Cr1TiKaL explores an underrated FPS game that’s being overlooked.",
                    Url = "https://www.youtube.com/watch?v=0Qr6rhbAzh0",
                    ViewCount = 955000,         
                    LikeCount = 155000,        
                    DislikeCount = 1000,
                    YoutuberID = 1
                },
                new Video
                {
                    Id = 2,
                    Title = "Are We Completely Cooked",
                    Description = "Cr1TiKaL asks—are we completely cooked? (food-themed humor).",
                    Url = "https://www.youtube.com/watch?v=KAKEPxVeTrE",
                    ViewCount = 2100000,        
                    LikeCount = 250000,
                    DislikeCount = 1000,
                    YoutuberID = 1
                },

                new Video
                {
                    Id = 3,
                    Title = "Building 5 PCs in the 5 BEST selling Cases",
                    Description = "Building five PCs in the best-selling cases—Linus Tech Tips.",
                    Url = "https://www.youtube.com/watch?v=iOC7-GPWxaU",
                    ViewCount = 715000,
                    LikeCount = 215000,
                    DislikeCount = 1000,
                    YoutuberID = 2
                },
                new Video
                {
                    Id = 4,
                    Title = "He won this $5000 Gaming PC (But it was VERY Awkward)",
                    Description = "A winner got a $5000 gaming PC—awkward moments unfold.",
                    Url = "https://www.youtube.com/watch?v=gXVpAVwN8F0",
                    ViewCount = 919000,
                    LikeCount = 317000,
                    DislikeCount = 1000,
                    YoutuberID = 2
                }
            );
        }
    }
}