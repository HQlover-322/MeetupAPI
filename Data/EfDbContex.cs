using Meetup.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meetup.Data
{
    public class EfDbContex: DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public EfDbContex(DbContextOptions options) : base(options)
        {
        }
        public EfDbContex()
        {

        }
    }
}
    