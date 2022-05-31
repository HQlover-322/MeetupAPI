using AutoMapper;
using Meetup.Data;
using Meetup.Data.Entities;
using Meetup.Models;
using Microsoft.EntityFrameworkCore;

namespace Meetup.Services
{
    public class SpeakerService
    {
        private readonly EfDbContex _dbContex;
        private readonly IMapper _mapper;
        public SpeakerService(EfDbContex dbContex, IMapper mapper)
        {
            _dbContex = dbContex;
            _mapper = mapper;
        }
        public async Task<List<SpeakerViewModel>> GetSpeakers()
        {
            var speakers = await _dbContex.Speakers.ToListAsync();
            return speakers.Select(x => _mapper.Map<SpeakerViewModel>(x)).ToList();
        }
        public async Task<SpeakerViewModel> GetSpeaker(Guid id)
        {
            var speaker = await _dbContex.Speakers.FindAsync(id);
            if (speaker is not null)
                return _mapper.Map<SpeakerViewModel>(speaker);
            return null;
        }
        public async Task AddNewSpeaker(SpeakerViewModel speakerView)
        {
            using (_dbContex)
            {
                var item = _mapper.Map<Speaker>(speakerView);
                _dbContex.Speakers.Add(item);
                _dbContex.SaveChanges();
            }
        }
        public async Task UpdateSpeaker(SpeakerViewModel speakerView)
        {
            using (_dbContex)
            {
                var item = await _dbContex.Speakers.FindAsync(speakerView.SpeakerId);
                item = _mapper.Map<Speaker>(speakerView);
                _dbContex.Update(item);
                _dbContex.SaveChanges();
            }
        }
        public async Task DeleteSpeaker(Guid id)
        {
            using (_dbContex)
            {
                var item = await _dbContex.Speakers.FindAsync(id);
                if (item is not null)
                {
                    _dbContex.Remove(item);
                    _dbContex.SaveChanges();
                }
            }
        }
    }
}
