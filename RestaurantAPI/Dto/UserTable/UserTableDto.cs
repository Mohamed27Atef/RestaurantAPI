namespace RestaurantAPI.Dto
{
    public class UserTableDto
    {
        public int reservationNumber { get; set; }
        public DateTime dateTime { get; set; } = DateTime.Now;
        public int tableNumber { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public int duration { get; set; }
    }
}
