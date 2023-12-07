using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Services;

public interface IAccountService
{
    void RegisterUser(RegisterUserDto registerUserDto);
    string GenerateJwt(LoginDto dto);
}