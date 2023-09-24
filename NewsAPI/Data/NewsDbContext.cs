using Microsoft.EntityFrameworkCore;
using NewsAPI.Models;

namespace NewsAPI.Data
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var categories = new List<Category> {
                new Category { Id = 1,Name="World"},
                new Category { Id = 2,Name="Politics"},
                new Category { Id = 3,Name="Climate"},
                new Category { Id = 4,Name="Science & Tech"},
                new Category { Id = 5,Name="Business"},
            };
            var news = new List<News> {
                 new News { Id = 1,  Title="news of Egypt",
                  Content ="news of worldnews of worldnews of worldnews of worldnews of world",
                  Writer ="Ali",
                  ImageUrl ="nop",
                  PublishDate =DateTime.Now,
                  CategoryId =2,
                },
                  new News { Id = 2,  Title="news of world",
                  Content ="news of worldnews of worldnews of worldnews of worldnews of world",
                  Writer ="Ali",
                  ImageUrl ="nop",
                  PublishDate =DateTime.Now,
                  CategoryId =1,
                },
                   new News { Id = 3,  Title="news of Climate",
                  Content ="news of worldnews of worldnews of worldnews of worldnews of world",
                  Writer ="Ali",
                  ImageUrl ="nop",
                  PublishDate =DateTime.Now,
                  CategoryId =3,
                }
            };
            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<News>().HasData(news);
        }
    }
}
