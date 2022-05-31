using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Meetup.Models;
using Meetup.Services;

namespace Meetup.Controllers;

[Route("api/[controller]")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly EventService _eventService;

    public HomeController(ILogger<HomeController> logger, EventService eventService)
    {
        _logger = logger;
        _eventService = eventService;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _eventService.GetEvents());
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var eventt = await _eventService.GetEvent(id);
        if (eventt is not null)
        return Ok(eventt);
        return BadRequest();
    }
    [HttpPost]
    public async Task<IActionResult> Post()
    {
        return Ok();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put()
    {
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok();
    }
}
