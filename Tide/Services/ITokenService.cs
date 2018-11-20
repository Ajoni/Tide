using System.Collections.Generic;
using System.Security.Claims;

namespace Tide.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        string GenerateConfirmationToken(string email);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
