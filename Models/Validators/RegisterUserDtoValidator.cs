using FluentValidation;
using PriceNegotiationApp.Data;

namespace PriceNegotiationApp.Models.Validators;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator(PriceNegotiationDbContext dbContext)
    {
        
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x=> x.Email).Custom((value, context) =>
        {
            var emailInUse = dbContext.Users.Any(u => u.Email == value);
            if (emailInUse)
            {
                context.AddFailure("Email", "The email address already exists is already in use.");
            }
        });
    }
}