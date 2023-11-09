using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Dto.ResturantFeedback
{
    public class ResturantFeedbackDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Feedback text is required.")]
        public string Text { get; set; }

        [Range(1, 5, ErrorMessage = "Rate must be between 1 and 5.")]
        public decimal Rate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Post Date")]
        public DateTime PostDate { get; set; }
        public string   userName { get; set; }

        public int ResturantId { get; set; }
    }

}
