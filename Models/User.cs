using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public string Name { get; set; }
    // Navigation properties
    public Organizer? Organizer { get; set; }
}