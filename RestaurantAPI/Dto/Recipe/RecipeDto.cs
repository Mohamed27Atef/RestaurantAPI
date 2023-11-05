using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RestaurantAPI.Models;
namespace RestaurantAPI.Dto
{
    public class RecipeDto
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        public string imageUrl { get; set; }
        public string menuName { get; set; }
        public int menuId { get; set; }
        public List<string> images { get; set; } = new List<string>();
        public string restaurantName { get; set; }
        public int restaurantId { get; set; }
        public decimal rate { get; set; }


    }
}
