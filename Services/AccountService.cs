using Microsoft.IdentityModel.Tokens;
using PriceNegotiationApp.Data;
using PriceNegotiationApp.Exceptions;
using PriceNegotiationApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace PriceNegotiationApp.Services;

public class AccountService : IAccountService
{
    private readonly PriceNegotiationDbContext _dbContext;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly AuthenticationSettings _authenticationSettings;

    public AccountService(PriceNegotiationDbContext dbContext,IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
    {
        _dbContext = dbContext;
        _authenticationSettings = authenticationSettings;
        _passwordHasher= passwordHasher;
    }

    public void RegisterUser(RegisterUserDto dto)
    {
        var newUser = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Password = dto.Password,
            RoleId = 2,
        };
        var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
        newUser.Password = hashedPassword;
        _dbContext.Users.Add(newUser);
        _dbContext.SaveChanges();
    }

    public string GenerateJwt(LoginDto dto)
    {
        var user = _dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefault(u => u.Email == dto.Email && u.Password == dto.Password);
        if (user is null)
        {
            throw new BadRequestException("Invalid username or password");
        }
        var claims = new List<Claim>()
        {
            new Claim (ClaimTypes.NameIdentifier,  user.Id.ToString()),
            new Claim (ClaimTypes.Email,  user.Email),
            new Claim (ClaimTypes.Role,  user.Role.RoleName),
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        var creed = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
        var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: credentials
        );
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
    public static bool IsAdmin(IEnumerable<Claim> claimsPrincipal)
    {
        var role = claimsPrincipal.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
        return role;
    }
}