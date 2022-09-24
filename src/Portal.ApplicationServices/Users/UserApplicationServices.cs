using Portal.ApplicationServices.Common;
using Portal.Domain;
using Portal.Domain.Users;
using Portal.Domain.Users.Contracts;
using Portal.Domain.Users.DTOs;

namespace Portal.ApplicationServices.Users;

public class UserApplicationServices : UserService
{
    private readonly UserRepository _userRepository;
    private readonly UnitOfWork _unitOfWork;
    public UserApplicationServices(UserRepository userRepository, 
                                   UnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool IsSuccess, string Message)> Register(RegisterDto register)
    {
        if (await _userRepository.IsExistByUserName(register.UserName))
            return (false, "Dublicate username");
        var newUser= CreateNewUser(register);
        await _userRepository.Add(newUser);
        await _unitOfWork.CommitAsync();
        return (true, "Successfully registered");
    }

    private static User CreateNewUser(RegisterDto register)
    {
        var newUser = new User
        {
            FirstName = register.FirstName.Trim().ToLower(),
            LastName = register.LastName.Trim().ToLower(),
            UserName = register.UserName.Trim().ToLower(),
            PasswordHash = HashHelper.EncryptString(register.Password),
        };
        return newUser;
    }

    public async Task<User> LogIn(LogInDto logIn)
    {
        return  await _userRepository
            .LogIn(logIn.UserName,HashHelper.EncryptString(logIn.Password));
    }
}
