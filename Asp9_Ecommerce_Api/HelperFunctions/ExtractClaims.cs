using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Asp9_Ecommerce_Api.HelperFunctions
{
    public static class ExtractClaims
    {
        public static int? ExtractUserIdFromToken(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return null; // Token is missing
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);

                var userIdClaim = jwtToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    return userId;
                }

                return null; // Invalid token or claim not found
            }
            catch
            {
                return null; // Token parsing failed
            }
        }
    }
}
