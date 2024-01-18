using Microsoft.IdentityModel.Tokens;
using PriceNegotiationApp.Data;
using PriceNegotiationApp.Exceptions;
using PriceNegotiationApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PriceNegotiationApp.Services{

public interface IAccountService
{
    Task RegisterUser(RegisterUserDto registerUserDto);
    Task<string> Login(LoginDto loginDto);
    string GenerateJwt(LoginDto dto);
}

public class AccountService : IAccountService
{
    private readonly PriceNegotiationDbContext _dbContext;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly AuthenticationSettings _authenticationSettings;

    public AccountService(PriceNegotiationDbContext dbContext, IPasswordHasher<User> passwordHasher,
        AuthenticationSettings authenticationSettings)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _authenticationSettings = authenticationSettings;
    }

    public async Task RegisterUser(RegisterUserDto dto)
    {
        var newUser = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Password = dto.Password,
            RoleId = 2, //dto.RoleId to register an admin
        };
        var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
        newUser.Password = hashedPassword;
        await _dbContext.Users.AddAsync(newUser);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<string> Login(LoginDto dto)
    {
        var user = await _dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == dto.Email) ??
                   throw new NotFoundException("Wrong email or password");
        var comparePassword = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
        if (comparePassword != PasswordVerificationResult.Success)
        {
            throw new NotFoundException("Wrong email or password");
        }

        var token = GenerateJwt(dto);
        return token;
    }

    public string GenerateJwt(LoginDto dto)
    {
        var user = _dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefault(u => u.Email == dto.Email);

        if (user is null)
        {
            throw new BadRequestException("Invalid email address or password");
        }

        var comparePassword = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
        if (comparePassword != PasswordVerificationResult.Success)
        {
            throw new BadRequestException("Invalid email address or password");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim(ClaimTypes.Role, $"{user.Role.RoleName}"),
            new Claim(ClaimTypes.Email, user.Email),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

        var token = new JwtSecurityToken(
            _authenticationSettings.JwtIssuer,
            _authenticationSettings.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: credentials
        );
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}
}