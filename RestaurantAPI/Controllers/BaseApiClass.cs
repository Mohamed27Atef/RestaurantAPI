using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiClass: ControllerBase
    {
        internal string GetUserIdFromClaims()
        {
            var Identifier = User.FindFirst(ClaimTypes.NameIdentifier);
            var UserId = Identifier.Value;
            return UserId;
        }
    }
}
