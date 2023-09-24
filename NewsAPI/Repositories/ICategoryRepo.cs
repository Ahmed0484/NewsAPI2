using NewsAPI.Models;

namespace NewsAPI.Repositories
{
    public interface ICategoryRepo
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task CreateAsync(Category category);
        Task<Category> UpdateAsync(int id ,Category category);
        Task<Category> DeleteAsync(int id);
    }
}
