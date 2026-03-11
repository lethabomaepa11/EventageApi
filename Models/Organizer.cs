public class Organizer
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    // Navigation property
    public List<Event> Events { get; set; }
}