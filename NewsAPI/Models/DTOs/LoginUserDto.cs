using System.ComponentModel.DataAnnotations;

namespace NewsAPI.Models.DTOs
{
    public class LoginUserDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
