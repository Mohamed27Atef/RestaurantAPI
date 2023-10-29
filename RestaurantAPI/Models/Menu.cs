using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public class Menu
    {
        public int id { get; set; }
        public string title { get; set; }
        public virtual List<Recipe>? Recipes { get; set; } = new List<Recipe>();

        [ForeignKey("restaurant")]
        public int restaurantId { get; set; }

        public virtual Resturant? restaurant { get; set; }
    }
}
