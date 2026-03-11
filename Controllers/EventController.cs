using Microsoft.AspNetCore.Mvc;
using EventAgeAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace EventageApi.Controllers;


[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Event>>> GetAllEvents()
    {
        //everyone can access this endpoint
        var events = await _eventService.GetAllEventsAsync();
        return Ok(events);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetEventById(int id)
    {
        var eventItem = await _eventService.GetEventByIdAsync(id);
        if (eventItem == null) return NotFound();
        return Ok(eventItem);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Event>> CreateEvent(CreateEventDto newEvent)
    {
        string userId = User.GetUserId();

        if (userId == null)
        {
            return Unauthorized("User ID not found in claims.");
        }
        ;

        //add the user ID to the event DTO, so we can associate the event with the user who created it
        var createdEvent = await _eventService.CreateEventAsync(newEvent, userId);
        return CreatedAtAction(nameof(GetEventById), new { id = createdEvent.Id }, createdEvent);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<Event>> UpdateEvent(int id, Event updatedEvent)
    {
        string userId = User.GetUserId();
        if (userId == null)
        {
            return Unauthorized("User ID not found in claims.");
        }
        ;
        var eventToUpdate = await _eventService.UpdateEventAsync(id, updatedEvent);
        if (eventToUpdate == null) return NotFound();
        return Ok(eventToUpdate);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEvent(int id)
    {
        string userId = User.GetUserId();
        if (userId == null)
        {
            return Unauthorized("User ID not found in claims.");
        }
        ;
        var result = await _eventService.DeleteEventAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}
