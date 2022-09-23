using Portal.Domain.Users;
using Portal.EF;

namespace Portal.Test.Factories;

public static class UserFactory
{
    public static User GenerateUser(EFdbApplication context)
    {
        var user = new User
        {
            FirstName = "Dummy-firstName",
            LastName = "Dummy-lastName",
            UserName = "Dummy-username"
        };
        context.Users.Add(user);
        return user;
    }
}
