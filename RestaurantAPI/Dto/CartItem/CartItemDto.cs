using RestaurantAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Dto.CartItem
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public  string restaurantName { get; set; }
        public  string recipeName { get; set; }
        public string recipeDescription { get; set; }
        public decimal recipePrice { get; set; }
        public int recipeId { get; set; }
        public string imageUrl { get; set; }

    }
}
