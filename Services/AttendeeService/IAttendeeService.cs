
namespace EventAgeAPI.Services;

public interface IAttendeeService
{
    Task<List<Attendee>> GetAllAttendeesAsync();
    Task<Attendee> GetAttendeeByIdAsync(int id);
    Task<Attendee> CreateAttendeeAsync(CreateAttendeeDto newAttendee);
    Task<Attendee> UpdateAttendeeAsync(int id, Attendee updatedAttendee);
    Task<bool> DeleteAttendeeAsync(int id);
}