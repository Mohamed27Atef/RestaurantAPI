using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public class Copon
    {
        public int id { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Number of users must be a non-negative integer.")]
        public int NumberOfUser { get; set; }

        [Required(ErrorMessage = "Coupon text is required.")]
        public string Text { get; set; }

        [Range(0, 100, ErrorMessage = "Discount percentage must be between 0 and 100.")]
        public decimal DiscountPercentage { get; set; }

        //[ForeignKey("Order")]
        //public int OrderId { get; set; }

        //public virtual Order? Order { get; set; }
    }
}
