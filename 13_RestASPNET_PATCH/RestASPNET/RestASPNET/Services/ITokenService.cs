using System.Collections.Generic;
using System.Security.Claims;

namespace RestASPNET.Services
{
    interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
