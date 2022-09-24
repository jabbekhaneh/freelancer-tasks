using FluentAssertions;
using Portal.ApplicationServices.Users;
using Portal.Domain;
using Portal.Domain.Users.Contracts;
using Portal.EF;
using Portal.EF.Users;
using Portal.Test.Factories;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Portal.Test.Portal.ApplicationServices.Users;

public class UserApplicationServicesTest
{
    private EFInMemoryDatabase _memoryContext;
    private EFdbApplication _context;
    private UserRepository _userRepository;
    private UnitOfWork _unitOfWork;
    private UserService _userService;
    public UserApplicationServicesTest()
    {
        _memoryContext = new EFInMemoryDatabase();
        _context = _memoryContext.CreateDataContext<EFdbApplication>();
        _unitOfWork =new EFUnitOfWork(_context);
        _userRepository= new EFUserRepository(_context);
        _userService=new  UserApplicationServices(_userRepository,_unitOfWork);
    }
    [Fact]
    private async Task Register()
    {
        var registerDto = UserFactory.GenerateRegisterDto();

        var result= await _userService.Register(registerDto);

        result.IsSuccess.Should().BeTrue();
        var getUser = _context.Users.First();
        getUser.UserName.Should().Be(registerDto.UserName);
        getUser.LastName.Should().Be(registerDto.LastName);
        getUser.FirstName.Should().Be(registerDto.FirstName);

    }


    [Fact]
    private async Task Login()
    {
       var user = UserFactory.GenerateUser(_context);
        _context.SaveChanges();
        var loginDto = UserFactory.GenerateLogInDto(user.UserName,"123456");

        var getUser = await _userService.LogIn(loginDto);

        
        getUser.UserName.Should().Be(user.UserName);
        getUser.LastName.Should().Be(user.LastName);
        getUser.FirstName.Should().Be(user.FirstName);

    }
}
