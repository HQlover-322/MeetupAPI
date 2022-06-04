using FluentValidation;
using Meetup.Data.Entities;
using Meetup.Models.Event;

namespace Meetup.Validators
{
    public sealed class EventPostValidator: AbstractValidator<EventPostModel>
    {
        public EventPostValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter name");
            RuleFor(x => x.EventOrganizerId).NotEmpty();
            RuleFor(x => x.EventPlaceId).NotEmpty();
            RuleFor(x => x.EventSpeakerId).NotEmpty();
            RuleFor(x => x.Time).NotEmpty().Must(time => time > DateTime.Now)
               .WithMessage("Wrong date or time");
        }
    }
    public class EventPutValidator : AbstractValidator<EventPutModel>
    {
        public EventPutValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter name");
            RuleFor(x => x.EventOrganizerId).NotEmpty();
            RuleFor(x => x.EventPlaceId).NotEmpty();
            RuleFor(x => x.EventSpeakerId).NotEmpty();
            RuleFor(x => x.Time).NotEmpty().Must(time=>time>DateTime.Now)
                .WithMessage("Wrong date or time");
        }
    }
}
