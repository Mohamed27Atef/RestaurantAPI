namespace RestaurantAPI.Dto.Order
{
    public class UserOrderRestualts
    {
        public string restaurantName { get; set; }
        // public List<UserOrders> userOrdersDto { get; set; } = new ();
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public string status { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string country { get; set; }
    }
}
