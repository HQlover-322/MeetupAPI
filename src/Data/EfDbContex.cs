using Meetup.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Meetup.Data
{
    public class EfDbContex: IdentityDbContext<User>
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Place> Places { get; set; }
        public EfDbContex(DbContextOptions<EfDbContex> options) : base(options)
        {
            //Database.EnsureCreated();
        }
        public EfDbContex()
        {

        }
    }
}
    