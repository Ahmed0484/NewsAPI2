using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NewsAPI.Data;
using NewsAPI.Models;

namespace NewsAPI.Repositories
{
    public class ImageRepo : IImageRepo
    {
        private readonly IWebHostEnvironment _webHostEnv;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly NewsDbContext _context;

        public ImageRepo(IWebHostEnvironment webHostEnv,
            IHttpContextAccessor httpContextAccessor,
            NewsDbContext context)
        {
            _webHostEnv = webHostEnv;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(_webHostEnv.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtension}");

            // Upload Image to Local Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // https://localhost:1234/images/image.jpg

            var urlFilePath = $@"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;


            // Add Image to the Images table
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();

            return image;
        }
    }
}
