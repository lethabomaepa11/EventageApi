using EventageAPI.Services;
using Microsoft.EntityFrameworkCore;
namespace EventAgeAPI.Services;

public class OrganizerService : IOrganizerService
{
    private readonly AppDbContext _context;
    public OrganizerService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Organizer> CreateOrganizerAsync(CreateOrganizer organizerDto)
    {
        var organizer = new Organizer
        {
            UserId = organizerDto.UserId,
            Id = organizerDto.UserId
        };

        _context.Organizers.Add(organizer);
        await _context.SaveChangesAsync();

        return organizer;
    }

    public async Task<Organizer> GetOrganizerByUserIdAsync(CreateOrganizer organizerDto)
    {
        return await _context.Organizers
            .Include(o => o.User) // Include related User data
            .FirstOrDefaultAsync(o => o.UserId == organizerDto.UserId);
    }
}