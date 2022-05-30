namespace Meetup.Models
{
    public class EventViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public OrganizerViewModel Organizer { get; set; }
        public SpeakerViewModel Speaker { get; set; }
        public DateTime DateTime { get; set; }
        public PlaceViewModel Place { get; set; }
    }
}
