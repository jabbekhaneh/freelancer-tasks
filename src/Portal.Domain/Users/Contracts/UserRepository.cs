using Portal.Domain.Users.DTOs;

namespace Portal.Domain.Users.Contracts;

public interface UserRepository
{
    Task<bool> IsExistByUserName(string userName);
    Task Add(User newUser);
    Task<User> LogIn(string username,string passwordHash);
}
