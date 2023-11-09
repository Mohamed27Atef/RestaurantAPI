using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestaurantAPI.Dto;
using RestaurantAPI.Dto.Account;
using RestaurantAPI.Dto.Account.cs;
using RestaurantAPI.Interfaces;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantAPI.Controllers
{

    public class AccountController : BaseApiClass
    {
        private readonly UserManager<ApplicationIdentityUser> userManager;
        private readonly IConfiguration config;
        private readonly IToken token;
        private readonly IUserRepository userRepository;
        private readonly IUserRepository iUserRepository;


        public AccountController(
            UserManager<ApplicationIdentityUser> _userManager,
            IConfiguration config,
            IToken token,
            IUserRepository userRepository)
        {
            userManager = _userManager;
            this.config = config;
            this.token = token;
            this.userRepository = userRepository;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            bool emailExists = await CheckIfEmailExists(userDto.Email);
            if (emailExists)
                return BadRequest("Email already exists");


            ApplicationIdentityUser user = new ApplicationIdentityUser()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Address = userDto.Address,
                UserName = userDto.Email.Split('@')[0],
                PhoneNumber = userDto.Phone,
                CreatedAt = DateTime.UtcNow
            };
            var result = await userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors.FirstOrDefault());

            User myUser = new User()
            {
                application_user_id = user.Id,
            };
            userRepository.Add(myUser);
            userRepository.SaveChanges();
            return Created("Account created successfuly", null);

        }
        [HttpPost("LogIn")]
        public async Task<ActionResult<UserDTOResult>> LogIn(LogInDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var user = await userManager.FindByEmailAsync(userDto.Email);
            if (user == null)
                return Unauthorized();

            var Succeeded = await userManager.CheckPasswordAsync(user, userDto.Password);

            if (!Succeeded)
                return Unauthorized();

            List<Claim> claims = await GetClaims(user);
            JwtSecurityToken Mytoken = token.generateToken(claims);

            return Ok(new UserDTOResult()
            {
                token = new JwtSecurityTokenHandler().WriteToken(Mytoken),
                imageUrl = userRepository.getUserImage(userRepository.getUserByApplicationUserId(user.Id).id),
                expiration = Mytoken.ValidTo
            });
        }


        private async Task<List<Claim>> GetClaims(ApplicationIdentityUser user)
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

            return claims;
        }
        private async Task<bool> CheckIfEmailExists(string email)
        {
            var existingUser = await userManager.FindByEmailAsync(email);
            return (existingUser != null);
        }



        [HttpGet("get-profile")]
        [Authorize]
        public async Task<ActionResult<UserProfile>> GetUserProfile()
        {
            string userId = GetUserIdFromClaims();
            var user = await userManager.FindByIdAsync(userId);
            User myUser = userRepository.getUserByApplicationUserId(GetUserIdFromClaims()) ;
            if (user == null)
                return NotFound();

            var userProfile = new UserProfile
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Location = user.Address,
                PhoneNumber = user.PhoneNumber,
                ProfileImage = userRepository.getUserImage(myUser.id),
            };

            return Ok(userProfile);
        }


        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile(UpdateProfileDto profileDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string userId = userRepository.getUserByApplicationUserId(GetUserIdFromClaims()).application_user_id;
            var user = await userManager.FindByIdAsync(userId);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();



            if (user != null)
            {
                user.UserName = profileDto.UserName;
                user.FirstName = profileDto.FirstName;
                user.LastName = profileDto.LastName;
                user.Email = profileDto.Email;
                user.Address = profileDto.Location;
                user.PhoneNumber = profileDto.PhoneNumber;
                user.User.Image = profileDto.ProfileImage;
                var result = await userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                }
            }

            return Ok("Profile updated successfully");


        }
    }
}
