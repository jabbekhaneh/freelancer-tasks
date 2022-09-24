using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.Users.DTOs
{
    public class LogInDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
