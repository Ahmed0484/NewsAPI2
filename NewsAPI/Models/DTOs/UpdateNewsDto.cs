using System.ComponentModel.DataAnnotations;

namespace NewsAPI.Models.DTOs
{
    public class UpdateNewsDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Writer { get; set; }
        public string ImageUrl { get; set; }
        [Range(typeof(DateTime), "20/9/2023", "20/10/2023",
      ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime PublishDate { get; set; }
        public int CategoryId { get; set; }
    }
}
