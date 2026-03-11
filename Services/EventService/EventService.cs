using Microsoft.EntityFrameworkCore;
namespace EventAgeAPI.Services;

public class EventService : IEventService
{
    private readonly AppDbContext _context;

    public EventService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Event>> GetAllEventsAsync()
    {
        return await _context.Events.ToListAsync();
    }
    public async Task<List<Event>> GetEventsByUserIdAsync(string userId)
    {
        return await _context.Events.Where(e => e.OrganizerId == userId).ToListAsync();
    }

    public async Task<Event> GetEventByIdAsync(int id)
    {
        return await _context.Events.FindAsync(id);
    }

    public async Task<Event> CreateEventAsync(CreateEventDto eventDto, string userId)
    {
        var newEvent = new Event
        {
            Title = eventDto.Title,
            Date = eventDto.Date,
            Location = eventDto.Location,
            OrganizerId = userId,
            Description = eventDto.Description
        };
        _context.Events.Add(newEvent);
        await _context.SaveChangesAsync();
        return newEvent;
    }

    public async Task<Event> UpdateEventAsync(int id, Event updatedEvent)
    {
        var existingEvent = await _context.Events.FindAsync(id);
        if (existingEvent == null) return null;

        existingEvent.Title = updatedEvent.Title;
        existingEvent.Date = updatedEvent.Date;
        existingEvent.Location = updatedEvent.Location;

        await _context.SaveChangesAsync();
        return existingEvent;
    }

    public async Task<bool> DeleteEventAsync(int id)
    {
        var eventToDelete = await _context.Events.FindAsync(id);
        if (eventToDelete == null) return false;

        _context.Events.Remove(eventToDelete);
        await _context.SaveChangesAsync();
        return true;
    }
}