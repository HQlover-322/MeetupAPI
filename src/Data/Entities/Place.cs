using Meetup.Data.Entities.Base;

namespace Meetup.Data.Entities
{
    public class Place:BaseEntity
    {
        public string PlaceName { get; set; }
        public string PlaceDescription { get; set; }
    }
}
