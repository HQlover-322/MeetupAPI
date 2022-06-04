using Meetup.DAO.Interfaces;
using Meetup.Data.Entities;
using Meetup.Models;
using Meetup.Models.Speaker;
using Microsoft.AspNetCore.Mvc;

namespace Meetup.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SpeakerController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly IBaseDAO<Speaker, SpeakerViewModel, SpeakerPostModel, SpeakerPutModel> _speakerDAO;
        public SpeakerController(ILogger<EventController> logger, IBaseDAO<Speaker, SpeakerViewModel, SpeakerPostModel, SpeakerPutModel> speakerDAO)
        {
            _logger = logger;
            _speakerDAO = speakerDAO;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var speakers = await _speakerDAO.GetAllAsync();
            return Ok(speakers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var speaker = await _speakerDAO.Get(id);
            if (speaker is not null)
            {
                return Ok(speaker);
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> Post(SpeakerPostModel speakerPost)
        {
            if (speakerPost is not null)
                return Ok(await _speakerDAO.Post(speakerPost));
            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> Put(SpeakerPutModel speakerPut)
        {
            if (ModelState.IsValid)
            {
                var istrue = await _speakerDAO.Put(speakerPut);
                if (istrue)
                    return Ok(istrue);
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var istrue = await _speakerDAO.Delete(id);
            if (istrue)
                return Ok(istrue);
            return BadRequest();
        }
    }
}
