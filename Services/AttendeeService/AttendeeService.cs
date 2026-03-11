using EventAgeAPI.Services;
using Microsoft.EntityFrameworkCore;
namespace EventageApi.Services;

public class AttendeeService : IAttendeeService
{
    private readonly AppDbContext _context;

    public AttendeeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Attendee> GetAttendeeByIdAsync(int id)
    {
        return await _context.Attendees.FindAsync(id);
    }

    public async Task<List<Attendee>> GetAllAttendeesAsync()
    {
        return await _context.Attendees.ToListAsync();
    }

    public async Task<Attendee> CreateAttendeeAsync(CreateAttendeeDto newAttendee)
    {
        var attendee = new Attendee
        {
            EventId = newAttendee.EventId
        };
        _context.Attendees.Add(attendee);
        await _context.SaveChangesAsync();
        return attendee;
    }

    public async Task<Attendee> UpdateAttendeeAsync(int id, Attendee updatedAttendee)
    {
        var existingAttendee = await _context.Attendees.FindAsync(id);
        if (existingAttendee == null) return null;

        existingAttendee.EventId = updatedAttendee.EventId;

        await _context.SaveChangesAsync();
        return existingAttendee;
    }

    public async Task<bool> DeleteAttendeeAsync(int id)
    {
        var attendeeToDelete = await _context.Attendees.FindAsync(id);
        if (attendeeToDelete == null) return false;

        _context.Attendees.Remove(attendeeToDelete);
        await _context.SaveChangesAsync();
        return true;
    }

}