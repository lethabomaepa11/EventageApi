using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public static class ClaimsService
{
    public static string GetUserId(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(JwtRegisteredClaimNames.Sub)
            ?? user.FindFirstValue(ClaimTypes.NameIdentifier)  // ← fallback
            ?? throw new UnauthorizedAccessException("User ID claim not found");
    }

    public static string GetEmail(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(JwtRegisteredClaimNames.Email)
            ?? throw new UnauthorizedAccessException("Email claim not found");
    }

    public static string GetUsername(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(JwtRegisteredClaimNames.UniqueName)
            ?? throw new UnauthorizedAccessException("Username claim not found");
    }
}