using Portal.Domain.Users.DTOs;

namespace Portal.Domain.Users.Contracts;

public interface UserService
{
    Task<(bool IsSuccess, string Message)> Register(RegisterDto register);
    Task<User> LogIn(LogInDto logIn);
}
