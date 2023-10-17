using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public class RecipeFeedback
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Feedback text is required.")]
        public string text { get; set; }

        [Range(1, 5, ErrorMessage = "Rate must be between 1 and 5.")]
        public decimal Rate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Post Date")]
        public DateTime PostDate { get; set; }

        [ForeignKey("User")]
        public int userId { get; set; }

        public virtual User? user { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe? Recipe { get; set; }

    }
}
