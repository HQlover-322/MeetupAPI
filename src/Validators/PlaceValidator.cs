using FluentValidation;
using Meetup.Models.Place;

namespace Meetup.Validators
{
    public sealed class PlacePostValidator : AbstractValidator<PlacePostModel>
    {
        public PlacePostValidator()
        {
            RuleFor(x => x.PlaceName).NotEmpty().WithMessage("Please enter name");
            RuleFor(x => x.PlaceDescription).NotEmpty();
        }
    }
    public sealed class PlacePutValidator : AbstractValidator<PlacePutModel>
    {
        public PlacePutValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.PlaceName).NotEmpty().WithMessage("Please enter name");
            RuleFor(x => x.PlaceDescription).NotEmpty();
        }
    }
}
