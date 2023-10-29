using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{

    public class ClosingDay
    {
        [Column(TypeName = "nvarchar(20)")]
        public DayOfWeek day { get; set; }

        [ForeignKey("Resturant")]
        public int restaurantId { get; set; }

        public virtual Resturant? Resturant { get; set; }

    }
}
