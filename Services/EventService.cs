using Meetup.Data;
using Meetup.Models;

namespace Meetup.Services
{
    public class EventService
    {
        private readonly EfDbContex dbContex;
        public EventService(EfDbContex dbContex)
        {
            this.dbContex = dbContex;
        }
        public async Task<List<EventViewModel>> GetViewModels()
        {

        }
    }
}
