using AutoMapper;
using Meetup.Data;
using Meetup.Data.Entities;
using Meetup.Models;
using Microsoft.EntityFrameworkCore;

namespace Meetup.Services
{
    public class EventService
    {
        private readonly EfDbContex _dbContex;
        private readonly IMapper _mapper;
        public EventService(EfDbContex dbContex, IMapper mapper)
        {
            _dbContex = dbContex;
            _mapper = mapper;
        }
        public async Task<List<EventViewModel>> GetEvents()
        {
            var events = await _dbContex.Events.ToListAsync();
            return  events.Select(x=> _mapper.Map<EventViewModel>(x)).ToList(); 
        }
        public async Task<EventViewModel> GetEvent(Guid id)
        {
            var eventt = await _dbContex.Events.FindAsync(id);
            if(eventt is not null)
                return _mapper.Map<EventViewModel>(eventt);
            return null;
        }
        public async Task AddNewEvent(EventViewModel eventView)
        {
            using(_dbContex)
            {
                var item = _mapper.Map<Event>(eventView);
                _dbContex.Events.Add(item);
                _dbContex.SaveChanges();
            }
        }
        public async Task UpdateEvent(EventViewModel eventView)
        {
            using(_dbContex)
            {
                var item = await _dbContex.Events.FindAsync(eventView.EventPlaceId);
                item = _mapper.Map<Event>(eventView);
                _dbContex.Update(item);
                _dbContex.SaveChanges();
            }
        }
        public async Task DeleteEvent(Guid id)
        {
            using(_dbContex)
            {
                var item = await _dbContex.Events.FindAsync(id);
                if (item is not null)
                {
                    _dbContex.Remove(item);
                    _dbContex.SaveChanges();
                }
            }
        }
    }
}
