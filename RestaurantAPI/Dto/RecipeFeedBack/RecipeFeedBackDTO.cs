using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Dto.RecipeFeedBack
{
    public class RecipeFeedbackDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Feedback text is required.")]
        public string Text { get; set; }

        [Range(1, 5, ErrorMessage = "Rate must be between 1 and 5.")]
        public decimal Rate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Post Date")]
        public DateTime PostDate { get; set; }

        public int UserId { get; set; }

        public int RecipeId { get; set; }
    }

}
