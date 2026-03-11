public class Attendee
{
    public int Id { get; set; }
    public string TicketId { get; set; } = "";
    public string UserId { get; set; } = "";
    public int EventId { get; set; }
    // Navigation property
    public Ticket Ticket { get; set; } = new Ticket();
}