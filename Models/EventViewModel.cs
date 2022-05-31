namespace Meetup.Models
{
    public class EventViewModel
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid EventOrganizerId { get; set; }
        public Guid EventSpeakerId { get; set; }
        public DateTime DateTime { get; set; }
        public Guid EventPlaceId { get; set; }
    }
}
