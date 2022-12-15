using FluentValidation;

namespace NzWalks.API.Validations
{
    public class RegionValidator : AbstractValidator<Models.DTO.RegionRequestBody>
    {
        public RegionValidator()
        {
            RuleFor(x=>x.Name).NotEmpty();
            RuleFor(x => x.Area).GreaterThan(0);
            RuleFor(x=>x.Code).NotEmpty();
            RuleFor(x=>x.Population).GreaterThan(0);
            RuleFor(x=>x.Lat).GreaterThan(0);
            RuleFor(x=>x.Long).GreaterThan(0);
        }
    }
}
