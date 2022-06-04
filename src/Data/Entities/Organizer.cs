using Meetup.Data.Entities.Base;

namespace Meetup.Data.Entities
{
    public class Organizer:BaseEntity
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
    }
}
