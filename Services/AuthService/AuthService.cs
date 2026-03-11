using EventageAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventAgeAPI.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly OrganizerService _organizerService;

    public AuthService(UserManager<User> userManager, IConfiguration configuration, IOrganizerService organizerService)
    {
        _userManager = userManager;
        _configuration = configuration;
        _organizerService = (OrganizerService)organizerService;
    }

    public async Task<string> RegisterAsync(RegisterDto user)
    {
        var newUser = new User { UserName = user.Email, Email = user.Email, Name = user.Name };

        var result = await _userManager.CreateAsync(newUser, user.Password);

        if (result.Succeeded)
        {
            // Optionally, you can add the user to a role here
            // await _userManager.AddToRoleAsync(user, "User");
            // If the user is also an organizer, you can create an organizer record here
            if (user.IsOrganizer)
            {
                await _organizerService.CreateOrganizerAsync(new CreateOrganizer(newUser.Id));
            }

            return "User registered successfully.";
        }
        else
        {
            return $"Registration failed: {string.Join(", ", result.Errors.Select(e => e.Description))}";
        }
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto user)
    {
        var existingUser = await _userManager.FindByEmailAsync(user.Email);
        if (existingUser is null)
        {
            Console.WriteLine($"Login failed: No user found with email {user.Email}");
            return null;
        }

        var isValidPassword = await _userManager.CheckPasswordAsync(existingUser, user.Password);
        if (!isValidPassword)
        {
            return null;
        }

        var token = await GenerateJwtTokenAsync(existingUser);
        return token;
    }

    private async Task<AuthResponseDto> GenerateJwtTokenAsync(User user)
    {
        var jwtSection = _configuration.GetSection("JWT");
        var secret = jwtSection["Secret"];
        var issuer = jwtSection["ValidIssuer"];
        var audience = jwtSection["ValidAudience"];
        var expiresMinutesValue = jwtSection["ExpiresMinutes"];

        if (string.IsNullOrWhiteSpace(secret))
        {
            throw new InvalidOperationException("JWT secret is not configured.");
        }

        var expiresMinutes = 60;
        if (int.TryParse(expiresMinutesValue, out var configuredMinutes) && configuredMinutes > 0)
        {
            expiresMinutes = configuredMinutes;
        }

        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiresAtUtc = DateTime.UtcNow.AddMinutes(expiresMinutes);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiresAtUtc,
            signingCredentials: creds
        );

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return new AuthResponseDto
        {
            Token = tokenValue,
            ExpiresAtUtc = expiresAtUtc,
            UserId = user.Id,
            Email = user.Email ?? string.Empty
        };
    }
}
