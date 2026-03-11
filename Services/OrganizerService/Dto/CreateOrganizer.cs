public class CreateOrganizer
{
    public string UserId { get; set; }

    public CreateOrganizer(string userId)
    {
        UserId = userId;
    }
}