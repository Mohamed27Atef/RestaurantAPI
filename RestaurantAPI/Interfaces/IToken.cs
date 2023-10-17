using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RestaurantAPI.Interfaces
{
    public interface IToken
    {
        JwtSecurityToken generateToken(List<Claim> claims);
    }
}
