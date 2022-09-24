namespace Portal.Domain.Users.Contracts;

public interface UserRepository
{
    Task<bool> IsExistByUserName(string userName);
    Task Add(User newUser);
}
