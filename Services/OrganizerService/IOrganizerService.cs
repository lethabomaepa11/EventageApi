namespace EventageAPI.Services;

public interface IOrganizerService
{
    Task<Organizer> CreateOrganizerAsync(CreateOrganizer organizerDto);
    Task<Organizer> GetOrganizerByUserIdAsync(CreateOrganizer organizerDto);
}