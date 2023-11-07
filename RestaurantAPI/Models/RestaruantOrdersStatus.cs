using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public class RestaruantOrdersStatus
    {
        public int id { get; set; }
        [ForeignKey("Cart")]
        public int cartId { get; set; }
        [ForeignKey("resturant")]
        public int restaurantId { get; set; }
        public OrderStatus status { get; set; }
        public virtual Cart? Cart { get; set; }
        public virtual Resturant? resturant { get; set; }
    }
}
