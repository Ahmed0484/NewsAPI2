using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Models;
using NewsAPI.Models.DTOs;
using NewsAPI.Repositories;

namespace NewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepo _repo;

        public ImagesController(IImageRepo repo)
        {
            _repo = repo;
        }
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDto imageDto)
        {
            ValidateFileUpload(imageDto);

            if (ModelState.IsValid)
            {
                // convert DTO to Domain model
                var image = new Image
                {
                    File = imageDto.File,
                    FileExtension = Path.GetExtension(imageDto.File.FileName),
                    FileSizeInBytes = imageDto.File.Length,
                    FileName = imageDto.FileName,
                    FileDescription = imageDto.FileDescription,
                };


                // User repository to upload image
                await _repo.Upload(image);

                return Ok(image);
               
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadDto imageDto)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(imageDto.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (imageDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file.");
            }
        }
    }
}
