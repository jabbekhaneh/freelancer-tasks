using Microsoft.EntityFrameworkCore;
using Portal.Domain.Users;
using Portal.Domain.Users.Contracts;

namespace Portal.EF.Users;

public class EFUserRepository : UserRepository
{
    private readonly EFdbApplication _context;

    public EFUserRepository(EFdbApplication context)
    {
        _context = context;
    }

    public async Task Add(User newUser)
    {
        await _context.Users.AddAsync(newUser);
    }

    public async Task<bool> IsExistByUserName(string userName)
    {
        return await _context.Users
            .AnyAsync(_=>_.UserName == userName);
    }
}
