using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class ApplicationIdentityUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual User? User { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }
    }
}
