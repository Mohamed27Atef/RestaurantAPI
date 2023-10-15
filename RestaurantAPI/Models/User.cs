using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RestaurantAPI.Models
{
    public class User : IdentityUser
    {

        [Required]
        public DateTime CreatedAt { get; set; }

        public string Image { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }

        [MaxLength(255)]
        public string Location { get; set; }



        [Required]
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public virtual Cart? Cart { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<RecipeFeedback> RecipeFeedbacks { get; set; } = new List<RecipeFeedback>();
        public ICollection<ResturantFeedback> ResturantFeedbacks { get; set; } = new List<ResturantFeedback>();


    }

}
