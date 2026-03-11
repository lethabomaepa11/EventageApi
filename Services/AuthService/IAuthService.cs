

namespace EventAgeAPI.Services
{
public interface IAuthService
{
    Task<string> RegisterAsync(RegisterDto user);
    Task<AuthResponseDto?> LoginAsync(LoginDto user);
}
}
