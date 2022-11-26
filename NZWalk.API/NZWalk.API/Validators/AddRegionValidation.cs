using FluentValidation;

namespace NZWalk.API.Validators
{
    public class AddRegionValidation : AbstractValidator<Models.DTO.AddRegionRequest>
    {
        public AddRegionValidation()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x=>x.Name).NotEmpty();
            RuleFor(x => x.Area).GreaterThan(0);
            RuleFor(x=>x.Population).GreaterThanOrEqualTo(0);
        }
    }
}
