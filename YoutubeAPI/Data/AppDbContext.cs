using Microsoft.EntityFrameworkCore;
using YoutubeAPI.Models.Entities;

namespace YoutubeAPI.Data
{
    public class AppDbContext : DbContext
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

            base.OnModelCreating(modelBuilder);
            SeedData(modelBuilder);
        }


        private void SeedData(ModelBuilder modelBuilder)
        {
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