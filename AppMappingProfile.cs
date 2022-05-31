using AutoMapper;
using Meetup.Data.Entities;
using Meetup.Models;

namespace Meetup
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Event, EventViewModel>().ReverseMap();
            CreateMap<Organizer, OrganizerViewModel>().ReverseMap();
            CreateMap<Speaker, SpeakerViewModel>().ReverseMap();
            CreateMap<Place, PlaceViewModel>().ReverseMap();
        }
    }
}
