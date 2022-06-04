using AutoMapper;
using Meetup.Data.Entities;
using Meetup.Models;
using Meetup.Models.Event;
using Meetup.Models.Organizer;
using Meetup.Models.Place;
using Meetup.Models.Speaker;

namespace Meetup
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Event, EventViewModel>().ReverseMap();
            CreateMap<EventPostModel, Event>();
            CreateMap<EventPutModel, Event>();

            CreateMap<Organizer, OrganizerViewModel>().ReverseMap();
            CreateMap<OrganizerPostModel, Organizer>();
            CreateMap<OrganizerPutModel, Organizer>();

            CreateMap<Speaker, SpeakerViewModel>().ReverseMap();
            CreateMap<SpeakerPostModel, Speaker>();
            CreateMap<SpeakerPutModel, Speaker>();

            CreateMap<Place, PlaceViewModel>().ReverseMap();
            CreateMap<PlacePostModel, Place>();
            CreateMap<PlacePutModel, Place>();
        }
    }
}
