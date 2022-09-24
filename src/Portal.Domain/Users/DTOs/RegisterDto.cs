using System.ComponentModel.DataAnnotations;

namespace Portal.Domain.Users.DTOs;

public class RegisterDto
{
    [Required,MaxLength(250)]
    public string FirstName { get; set; }
    [Required, MaxLength(250)]
    public string LastName { get; set; }
    [Required, MaxLength(10)]
    public string UserName { get; set; }
    [Required, MaxLength(100),MinLength(4)]
    public string Password { get; set; }
    [Compare("Password")]
    public string ConfirmedPassword { get; set; }
}
