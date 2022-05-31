using AutoMapper;
using Meetup.Data;
using Meetup.Data.Entities;
using Meetup.Models;
using Microsoft.EntityFrameworkCore;

namespace Meetup.Services
{
    public class OrganizerService
    {
        private readonly EfDbContex _dbContex;
        private readonly IMapper _mapper;
        public OrganizerService(EfDbContex dbContex, IMapper mapper)
        {
            _dbContex = dbContex;
            _mapper = mapper;
        }
        public async Task<List<OrganizerViewModel>> GetOrganizers()
        {
            var organizers = await _dbContex.Organizers.ToListAsync();
            return organizers.Select(x => _mapper.Map<OrganizerViewModel>(x)).ToList();
        }
        public async Task<OrganizerViewModel> GetOrganizer(Guid id)
        {
            var organizer = await _dbContex.Organizers.FindAsync(id);
            if (organizer is not null)
                return _mapper.Map<OrganizerViewModel>(organizer);
            return null;
        }
        public async Task AddNewOrganizer(OrganizerViewModel organizerView)
        {
            using (_dbContex)
            {
                var item = _mapper.Map<Organizer>(organizerView);
                _dbContex.Organizers.Add(item);
                _dbContex.SaveChanges();
            }
        }
        public async Task UpdateOrganizer(OrganizerViewModel organizerView)
        {
            using (_dbContex)
            {
                var item = await _dbContex.Organizers.FindAsync(organizerView.OrganizerId);
                item = _mapper.Map<Organizer>(organizerView);
                _dbContex.Update(item);
                _dbContex.SaveChanges();
            }
        }
        public async Task DeleteOrganizer(Guid id)
        {
            using (_dbContex)
            {
                var item = await _dbContex.Organizers.FindAsync(id);
                if (item is not null)
                {
                    _dbContex.Remove(item);
                    _dbContex.SaveChanges();
                }
            }
        }
    }
}
