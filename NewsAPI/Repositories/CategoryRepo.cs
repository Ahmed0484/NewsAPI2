using Microsoft.EntityFrameworkCore;
using NewsAPI.Data;
using NewsAPI.Models;
using NewsAPI.Models.DTOs;

namespace NewsAPI.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly NewsDbContext _context;

        public CategoryRepo(NewsDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            
        }

        public async Task<Category> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> UpdateAsync(int id, Category category)
        {
            var currentCategory = await _context.Categories.FindAsync(id);
            if (currentCategory == null) return null;
            currentCategory.Name = category.Name;
            await _context.SaveChangesAsync();

            return currentCategory;
        }
    }
}
