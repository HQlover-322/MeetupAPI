namespace Meetup.Data.Entities
{
    public class Organizer
    {
        public Guid OrganizerId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
    }
}
