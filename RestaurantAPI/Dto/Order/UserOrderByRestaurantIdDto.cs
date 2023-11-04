using RestaurantAPI.Models;

namespace RestaurantAPI.Dto.Order
{
    public class UserOrderByRestaurantIdDto
    {
        public string restaurantName { get; set; }
        //public List<Models.Order> userOrders { get; set; } = new List<Models.Order> ();
        public int Id { get; set; }
        public int restaurantId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public string status { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string country { get; set; }
    }
}
