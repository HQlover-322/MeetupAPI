namespace Meetup.Data.Entities
{
    public class Event
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Organizer EventOrganizer { get; set; }
        public Guid EventOrganizerId { get; set; }
        public Speaker EventSpeaker { get; set; }
        public Guid EventSpeakerId { get; set; }
        public DateTime Time { get; set; }
        public Place EventPlace { get; set; }
        public Guid EventPlaceId { get; set; }
    }
}
