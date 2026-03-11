public class Ticket
{
    public string Id { get; set; }
    public int EventId { get; set; }
    public decimal Price { get; set; }
    // Navigation property
    public Event Event { get; set; }
}