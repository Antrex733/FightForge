namespace FightForge.Models.Validators
{
    public class GymQueryValidator : AbstractValidator<GymQuery>
    {
        private int[] allowedPageSizes = {5, 10, 15, 20};
        private string[] allowedSortByPropertyNames = { nameof(Gym.Name), nameof(Gym.Description) };

        public GymQueryValidator()
        {
            RuleFor(r => r.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(r => r.PageSize)
                .Custom((value, context) =>
                {
                    if (!allowedPageSizes.Contains(value))
                    {
                        context.AddFailure("PageSize", $"PageSize must in [{string.Join(",", allowedPageSizes)}]");
                    }
                });

            RuleFor(x => x.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedSortByPropertyNames.Contains(value))
                .WithMessage($"SortBy is optional, or must be in [{string.Join(",", allowedSortByPropertyNames)}]");
        }
    }
}
