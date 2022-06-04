using FluentValidation;
using Meetup.Models.Event;
using Meetup.Models.Organizer;

namespace Meetup.Validators
{
    public sealed class OrganizerPostValidator : AbstractValidator<OrganizerPostModel>
    {
        public OrganizerPostValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter name");
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.FullName).NotEmpty();
        }
    }
    public sealed class OrganizerPutValidator : AbstractValidator<OrganizerPutModel>
    {
        public OrganizerPutValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter name");
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.FullName).NotEmpty();
        }
    }
}
