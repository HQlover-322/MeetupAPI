using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Meetup.Models;
using Meetup.Services;
using Meetup.DAO.Interfaces;
using Meetup.Data.Entities;
using AutoMapper;
using Meetup.Services.Interfaces;
using Meetup.Models.Event;

namespace Meetup.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class EventController : ControllerBase
{
    private readonly ILogger<EventController> _logger;
    private readonly IBaseDAO<Event,EventViewModel,EventPostModel,EventPutModel> _eventDAO;
    public EventController(ILogger<EventController> logger, IBaseDAO<Event, EventViewModel, EventPostModel, EventPutModel> eventDAO)
    {
        _logger = logger;
        _eventDAO = eventDAO;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var events = await _eventDAO.GetAllAsync(nameof(Event.EventOrganizer), nameof(Event.EventSpeaker), nameof(Event.EventPlace));
        return Ok(events);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var eventt = await _eventDAO.Get(id, nameof(Event.EventOrganizer), nameof(Event.EventSpeaker), nameof(Event.EventPlace));
        if (eventt is not null)
        {
            return Ok(eventt);
        }
        return BadRequest();
    }
    [HttpPost]
    public async Task<IActionResult> Post(EventPostModel eventPost)
    {
        if (eventPost is not null)
        return Ok(await _eventDAO.Post(eventPost));
        return BadRequest();
    }
    [HttpPut]
    public async Task<IActionResult> Put(EventPutModel eventPut)
    {
        if(eventPut is not null)
        {
            var istrue = await _eventDAO.Put(eventPut);
            if(istrue)
                return Ok(istrue);
        }
        return BadRequest();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var istrue= await _eventDAO.Delete(id);
        if(istrue)
            return Ok(istrue);
        return BadRequest();
    }
}
