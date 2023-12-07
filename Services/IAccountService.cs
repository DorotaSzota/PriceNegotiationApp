using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Services;

public interface IAccountService
{
    Task RegisterUser(RegisterUserDto registerUserDto);
    Task<string> Login(LoginDto dto);
    string CreateToken(User user);
}