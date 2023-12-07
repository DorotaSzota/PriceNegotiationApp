using Microsoft.IdentityModel.Tokens;
using PriceNegotiationApp.Data;
using PriceNegotiationApp.Exceptions;
using PriceNegotiationApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace PriceNegotiationApp.Services;

public class AccountService : IAccountService
{
    private readonly PriceNegotiationDbContext _dbContext;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IValidator<RegisterUserDto> _validator;
    private readonly IConfiguration _configuration; //???

    public AccountService(PriceNegotiationDbContext dbContext,IPasswordHasher<User> passwordHasher, IValidator<RegisterUserDto> validator,  IConfiguration configuration)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
        _validator = validator;
    }

    public async Task RegisterUser(RegisterUserDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
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
        _dbContext.Users.AddAsync(newUser);
        _dbContext.SaveChangesAsync();
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
        var token = CreateToken(user);
        return token;
    }
    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;
        if (appSettingsToken is null)
            throw new Exception("AppSettings Token is null");


        SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF32.GetBytes(appSettingsToken));

        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(3),
            SigningCredentials = credentials
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}