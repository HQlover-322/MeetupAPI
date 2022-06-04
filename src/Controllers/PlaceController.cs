using Meetup.DAO.Interfaces;
using Meetup.Data.Entities;
using Meetup.Models;
using Meetup.Models.Place;
using Microsoft.AspNetCore.Mvc;

namespace Meetup.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly IBaseDAO<Place, PlaceViewModel, PlacePostModel, PlacePutModel> _placeDAO;
        public PlaceController(ILogger<EventController> logger, IBaseDAO<Place, PlaceViewModel, PlacePostModel, PlacePutModel> placeDAO)
        {
            _logger = logger;
            _placeDAO = placeDAO;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var speakers = await _placeDAO.GetAllAsync();
            return Ok(speakers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var place = await _placeDAO.Get(id);
            if (place is not null)
            {
                return Ok(place);
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> Post(PlacePostModel placePost)
        {
            if (placePost is not null)
                return Ok(await _placeDAO.Post(placePost));
            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> Put(PlacePutModel placePut)
        {
            if (ModelState.IsValid)
            {
                var istrue = await _placeDAO.Put(placePut);
                if (istrue)
                    return Ok(istrue);
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var istrue = await _placeDAO.Delete(id);
            if (istrue)
                return Ok(istrue);
            return BadRequest();
        }
    }
}
