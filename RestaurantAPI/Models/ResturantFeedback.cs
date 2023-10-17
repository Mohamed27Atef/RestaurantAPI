using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public class ResturantFeedback
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Feedback text is required.")]
        public string text { get; set; }

        [Range(1, 5, ErrorMessage = "Rate must be between 1 and 5.")]
        public decimal Rate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Post Date")]
        public DateTime PostDate { get; set; }

        [ForeignKey("Resturant")]
        public int ResturantId { get; set; }
        public virtual Resturant? Resturant { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }

    }
}
