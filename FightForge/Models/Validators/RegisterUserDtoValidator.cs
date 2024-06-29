using FightForge.DTOs;

namespace FightForge.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(GymDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .MinimumLength(8);

            RuleFor(x => x.Password)//litera wielka, mala i cyfra
                .Equal(y => y.ConfirmPassword)
                .Custom((value, context) =>
                {
                    var characters = value.ToLower() == value || value.ToUpper() == value || !value.Any(char.IsDigit);
                    if (characters)
                    {
                        context.AddFailure("Password", "The password must contain at least one uppercase letter, one lowercase letter and a number");
                    }
                });

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var isTaken = dbContext.Users.Any(u => u.Email == value);
                    if (isTaken) 
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                });
        }
    }
}
