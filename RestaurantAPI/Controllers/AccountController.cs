using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<ApplicationIdentityUser> userManager;

		public AccountController(UserManager<ApplicationIdentityUser>userManager)
        {
			this.userManager = userManager;
		}
    }
}
