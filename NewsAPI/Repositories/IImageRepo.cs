using NewsAPI.Models;

namespace NewsAPI.Repositories
{
    public interface IImageRepo
    {
        Task<Image> Upload(Image image);
    }
}
