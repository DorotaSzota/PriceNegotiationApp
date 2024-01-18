using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Services;

public interface IAccountService
{
    Task RegisterUser(RegisterUserDto registerUserDto);
    Task<string> Login(LoginDto loginDto);
    string GenerateJwt(LoginDto dto);
}