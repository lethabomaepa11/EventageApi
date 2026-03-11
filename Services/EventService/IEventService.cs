namespace EventAgeAPI.Services;


public interface IEventService
{
    Task<List<Event>> GetAllEventsAsync();
    Task<List<Event>> GetEventsByUserIdAsync(string userId);
    Task<Event> GetEventByIdAsync(int id);
    Task<Event> CreateEventAsync(CreateEventDto newEvent, string userId);
    Task<Event> UpdateEventAsync(int id, Event updatedEvent);
    Task<bool> DeleteEventAsync(int id);
}