using RestaurantAPI.Models;

namespace RestaurantAPI.Dto.Order
{
    public class UserOrders
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public string status { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string country { get; set; }

    }
}
