namespace Meetup.Models.Event
{
    public class EventPostModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid EventOrganizerId { get; set; }
        public Guid EventSpeakerId { get; set; }
        public DateTime Time { get; set; }
        public Guid EventPlaceId { get; set; }
    }
}
