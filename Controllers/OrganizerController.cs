using AutoMapper;
using Meetup.DAO.Interfaces;
using Meetup.Data.Entities;
using Meetup.Models;
using Meetup.Models.Organizer;
using Microsoft.AspNetCore.Mvc;

namespace Meetup.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrganizerController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly IBaseDAO<Organizer, OrganizerViewModel, OrganizerPostModel, OrganizerPutModel> _organizerDAO;
        public OrganizerController(ILogger<EventController> logger, IBaseDAO<Organizer, OrganizerViewModel, OrganizerPostModel, OrganizerPutModel> organizerDAO)
        {
            _logger = logger;
            _organizerDAO = organizerDAO;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var organizers = await _organizerDAO.GetAllAsync();
            return Ok(organizers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var speaker = await _organizerDAO.Get(id);
            if (speaker is not null)
            {
                return Ok(speaker);
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> Post(OrganizerPostModel organizerPost)
        {
            if (organizerPost is not null)
                return Ok(await _organizerDAO.Post(organizerPost));
            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> Put(OrganizerPutModel organizerPut)
        {
            if (ModelState.IsValid)
            {
                var istrue = await _organizerDAO.Put(organizerPut);
                if (istrue)
                    return Ok(istrue);
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var istrue = await _organizerDAO.Delete(id);
            if (istrue)
                return Ok(istrue);
            return BadRequest();
        }
    }
}
