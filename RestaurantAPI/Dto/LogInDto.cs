using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Dto
{
	public class LogInDto
	{
		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Enter valid email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required.")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
