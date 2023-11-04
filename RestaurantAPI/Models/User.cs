using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RestaurantAPI.Models
{
    public class User
    {
        public int id { get; set; }
        [ForeignKey("ApplicationUser")]
        public string application_user_id { get; set; }
        public virtual ApplicationIdentityUser? ApplicationUser { get; set; }
       
        public string? Image { get; set; }


        [MaxLength(255)]
        public string? Location { get; set; }


        public virtual ICollection<UserTable>? userTable { get; set; }

        public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();
        public virtual ICollection<Cart>? carts { get; set; } = new List<Cart>();
        public virtual ICollection<RecipeFeedback>? RecipeFeedbacks { get; set; } = new List<RecipeFeedback>();
        public virtual ICollection<ResturantFeedback>? ResturantFeedbacks { get; set; } = new List<ResturantFeedback>();
    }

}
