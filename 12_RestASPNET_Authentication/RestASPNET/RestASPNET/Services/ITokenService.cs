using System.Collections.Generic;
using System.Security.Claims;

namespace RestASPNET.Services
{
    interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);

        string GenerateRefresToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
