using AutoMapper;
using Meetup.Data;
using Meetup.Data.Entities;
using Meetup.Models;
using Microsoft.EntityFrameworkCore;

namespace Meetup.Services
{
    public class PlaceService
    {
        private readonly EfDbContex _dbContex;
        private readonly IMapper _mapper;
        public PlaceService(EfDbContex dbContex, IMapper mapper)
        {
            _dbContex = dbContex;
            _mapper = mapper;
        }
        public async Task<List<PlaceViewModel>> GetPlaces()
        {
            var places = await _dbContex.Places.ToListAsync();
            return places.Select(x => _mapper.Map<PlaceViewModel>(x)).ToList();
        }
        public async Task<PlaceViewModel> GetPlace(Guid id)
        {
            var place = await _dbContex.Places.FindAsync(id);
            if (place is not null)
                return _mapper.Map<PlaceViewModel>(place);
            return null;
        }
        public async Task AddNewPlace(PlaceViewModel placeView)
        {
            using (_dbContex)
            {
                var item = _mapper.Map<Place>(placeView);
                _dbContex.Places.Add(item);
                _dbContex.SaveChanges();
            }
        }
        public async Task UpdatePlace(PlaceViewModel placeView)
        {
            using (_dbContex)
            {
                var item = await _dbContex.Places.FindAsync(placeView.PlaceId);
                item = _mapper.Map<Place>(placeView);
                _dbContex.Update(item);
                _dbContex.SaveChanges();
            }
        }
        public async Task DeletePlace(Guid id)
        {
            using (_dbContex)
            {
                var item = await _dbContex.Places.FindAsync(id);
                if (item is not null)
                {
                    _dbContex.Remove(item);
                    _dbContex.SaveChanges();
                }
            }
        }
    }
}
