using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class RecipeFeedBack
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int RecipeId { get; set; }

        [Required]
        [MaxLength(500)] 
        public string Text { get; set; }

        [Required]
        [Range(1, 5)] 
        public int Rate { get; set; }

        [Required]
        public DateTime PostDate { get; set; }

        public User User { get; set; }

        public Recipe Recipe { get; set; }
    }
}
