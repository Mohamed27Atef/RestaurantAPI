using RestaurantAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Dto
{
    public class MenuDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public int restaurantId { get; set; }

    }
}
