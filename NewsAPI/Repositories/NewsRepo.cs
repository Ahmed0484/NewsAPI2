using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Services;
using NewsAPI.Data;
using NewsAPI.Models;
using System.Globalization;

namespace NewsAPI.Repositories
{
    public class NewsRepo : INewsRepo
    {
        private readonly NewsDbContext _context;

        public NewsRepo(NewsDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(News news)
        {
            await _context.News.AddAsync(news);
            await _context.SaveChangesAsync();
            news.Category = await _context.Categories.FindAsync(news.CategoryId);
        }

        public Task<List<News>> GetAllAsync(string filterOn = null, string filterQuery = null,
            string sortBy = null, bool isAscending = true,int pageNumber = 1, int pageSize = 1000)
        {
            var news = _context.News.Include("Category").AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false &&
                    string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    news = news.Where(x => x.Title.Contains(filterQuery));
                }
            }
            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    news = isAscending ? news.OrderBy(x => x.Title) : news.OrderByDescending(x => x.Title);
                }
            }
            //Pagination
            var skipResults = (pageNumber-1)*pageSize;

            return news.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<News> GetByIdAsync(int id)
        {
            return await _context.News
                .Include("Category")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<News> UpdateAsync(int id, News news)
        {
            var currentNews = await _context.News.FindAsync(id);
            if (currentNews == null) return null;
            currentNews.Title = news.Title;
            currentNews.PublishDate = news.PublishDate;
            currentNews.Writer = news.Writer;
            currentNews.CategoryId = news.CategoryId;
            currentNews.Content = news.Content;
            currentNews.ImageUrl = news.ImageUrl;
            currentNews.Category = await _context.Categories.FindAsync(news.CategoryId);
            await _context.SaveChangesAsync();
            return currentNews;
        }

        public async Task<News> DeleteAsync(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null) return null;
            news.Category = await _context.Categories.FindAsync(news.CategoryId);
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return news;
        }
    }
}
