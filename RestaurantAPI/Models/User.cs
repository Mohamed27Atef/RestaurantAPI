using Microsoft.AspNetCore.Identity;

namespace RestaurantAPI.Models
{
	public class User:IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime CreatedAt { get; set; }
		public string? Image { get; set; }
		public string Address { get; set; }
		public string? Longitude { get; set; }
		public string? Latitude { get; set; }
	}
}
