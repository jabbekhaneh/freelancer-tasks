using Portal.ApplicationServices.Common;
using Portal.Domain.Users;
using Portal.Domain.Users.DTOs;
using Portal.EF;
using System;

namespace Portal.Test.Factories;

public static class UserFactory
{
    public static User GenerateUser(EFdbApplication context)
    {
        var user = new User
        {
            FirstName = "Dummy-firstName",
            LastName = "Dummy-lastName",
            UserName = "Dummy-username",
            PasswordHash =HashHelper.EncryptString("123456")
           
        };
        context.Users.Add(user);
        return user;
    }

    internal static RegisterDto GenerateRegisterDto()
    {
        return new RegisterDto
        {
            FirstName = "dummy-r-firstname",
            UserName = "dummy-r-username",
            LastName = "dummy-r-lastname",
            Password = "123456",
        };
    }

    internal static LogInDto GenerateLogInDto(string userName, string password)
    {
        return new LogInDto
        {
            UserName = userName,
            Password = password
        };
    }
}
