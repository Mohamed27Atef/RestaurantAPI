using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RestaurantAPI.Models
{
    public class RestaurantImage
    {
        public string imageUrl { get; set; }

        [ForeignKey("Resturant")]
        public int restaurantId { get; set; }

        public virtual Resturant? Resturant { get; set; }
    }
}
