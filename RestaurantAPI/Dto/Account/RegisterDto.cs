using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Dto
{
	public class RegisterDto
	{

		[Required(ErrorMessage = "First Name is required.")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Last Name is required.")]
		public string LastName { get; set; }
		
		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Enter valid email")]
		public string Email { get; set; }
		
		[Required(ErrorMessage = "Password is required.")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password is required.")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Confirm Password must match Password.")]
		public string ConfirmPassword { get; set; }

		[Required(ErrorMessage = "Phone Number is required.")]
		[RegularExpression(@"^\d{11}$", ErrorMessage = "Phone Number must be a 11-digit number.")]
		public string Phone { get; set; }
		
		[Required(ErrorMessage = "Address is required.")]
		public string Address { get; set; }

	}
}
