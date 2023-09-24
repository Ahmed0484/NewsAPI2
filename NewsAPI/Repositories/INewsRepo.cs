using NewsAPI.Models;

namespace NewsAPI.Repositories
{
    public interface INewsRepo
    {
        Task CreateAsync(News news);
        Task<List<News>> GetAllAsync(string filterOn = null, string filterQuery = null,
            string sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 1000);
        Task<News> GetByIdAsync(int id);
        Task<News> UpdateAsync(int id, News news);
        Task<News> DeleteAsync(int id);
    }
}
