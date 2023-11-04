namespace RestaurantAPI.Dto.Order
{
    public class OrderAdmin
    {
        public int orderId { get; set; }
        public decimal totalPrice { get; set; }

        public string customerName { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string street { get; set; }
        public DateTime date { get; set; } = DateTime.Now;
        public string status { get; set; }
        public string customerPhone { get; set; }

    }
}
