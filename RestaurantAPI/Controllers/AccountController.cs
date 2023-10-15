using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestaurantAPI.Dto;
using RestaurantAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<User> userManager;
		private readonly IConfiguration config;

		public AccountController(UserManager<User> _userManager,IConfiguration config)
        {
			userManager = _userManager;
			this.config = config;
		}
        [HttpPost("register")]
		public async Task<IActionResult> Register(RegisterDto userDto)
		{
            if ( ModelState.IsValid )
            {
				User user = new User()
				{
					FirstName = userDto.FirstName,
					LastName = userDto.LastName,
					Email = userDto.Email,
					Address = userDto.Address,
					UserName = userDto.Email.Split('@')[0],
					PhoneNumber = userDto.Phone,
					CreatedAt = DateTime.UtcNow
				};
				var result =await userManager.CreateAsync(user,userDto.Password);
				if (result.Succeeded)
					return Ok("Account created successfuly");
				else
					return BadRequest(result.Errors.FirstOrDefault());
				
            }
			return BadRequest(ModelState);
        }
		[HttpPost("LogIn")]
		public async Task<IActionResult> LogIn(LogInDto userDto)
		{
			if (ModelState.IsValid)
			{
				var user = await userManager.FindByEmailAsync(userDto.Email);
				if (user != null)
				{
					var result = await userManager.CheckPasswordAsync(user, userDto.Password);
					if (result)
					{
						var claims = new List<Claim>()
						{
							new Claim(ClaimTypes.Name,user.UserName),
							new Claim(ClaimTypes.NameIdentifier,user.Id),
							new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
						};
						var roles = await userManager.GetRolesAsync(user);
						foreach (var role in roles)
						{
							claims.Add(new Claim(ClaimTypes.Role, role));
						}
						SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:srcret"]));
						SigningCredentials signingCredentials = new SigningCredentials(
							securityKey, SecurityAlgorithms.HmacSha256
							);
						JwtSecurityToken Mytoken = new JwtSecurityToken(
							issuer: config["JWT:ValidIssuer"],
							audience: config["JWT:ValidAudiance"],
							claims: claims,
							expires: DateTime.Now.AddHours(1),
							signingCredentials: signingCredentials
							);
						return Ok(new
						{
							token = new JwtSecurityTokenHandler().WriteToken(Mytoken),
							expiration = Mytoken.ValidTo
						});						      
					}
				}
			}
			return Unauthorized();
		}
	}
}
