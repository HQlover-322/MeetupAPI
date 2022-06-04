using FluentValidation;
using Meetup.Models.Speaker;

namespace Meetup.Validators
{
    public sealed class SpeakerPostValidator : AbstractValidator<SpeakerPostModel>
    {
        public SpeakerPostValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter name");

        }
    }
    public sealed class SpeakerPutValidator : AbstractValidator<SpeakerPutModel>
    {
        public SpeakerPutValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter name");
        }
    }
}
