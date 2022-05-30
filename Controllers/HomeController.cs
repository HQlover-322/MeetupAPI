using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Meetup.Models;

namespace Meetup.Controllers;

[Route("api/[controller]")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok();
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
