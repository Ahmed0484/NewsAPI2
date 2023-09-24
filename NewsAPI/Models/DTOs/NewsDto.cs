using System.ComponentModel.DataAnnotations;

namespace NewsAPI.Models.DTOs
{
    public class NewsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Writer { get; set; }
        public string ImageUrl { get; set; }
       
        public DateTime PublishDate { get; set; }
        public CategoryDto Category { get; set; }
    }
}
