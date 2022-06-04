using Meetup.Models.Organizer;

namespace Meetup.Models
{
    public class EventViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public OrganizerViewModel EventOrganizer { get; set; }
        public SpeakerViewModel EventSpeaker { get; set; }
        public DateTime Time { get; set; }
        public PlaceViewModel EventPlace { get; set; }
    }
}
