public class AuthResponseDto
{
    public string Token { get; set; }
    public DateTime ExpiresAtUtc { get; set; }
    public string UserId { get; set; }
    public string Email { get; set; }
}
