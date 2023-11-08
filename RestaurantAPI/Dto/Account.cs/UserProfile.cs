using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Dto.Account.cs
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfileImage { get; set; }
    }

}
