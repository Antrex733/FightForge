namespace FightForge.Models.Validators
{
    public class CreateSportDtoValidator : AbstractValidator<CreateSportDto>
    {
        public CreateSportDtoValidator(GymDbContext dbContext)
        {
            RuleFor(x => x.TrainerId)
                .Custom((value, context) =>
                {
                    var trainer = dbContext.Users.FirstOrDefault(x => x.Id == value);
                    if (trainer == null || trainer.RoleId != 2)
                    {
                        context.AddFailure("RoleId", "Trainer role is required");
                    }
                });
        }
    }
}
