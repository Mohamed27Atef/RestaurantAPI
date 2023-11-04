namespace RestaurantAPI.Dto.UserTable
{
    public class RestauarantAdminReservationDto
    {
        public int reservationNumber { get; set; }
        public DateTime dateTime { get; set; } = DateTime.Now;
        public int tableNumber { get; set; }
        public string tableType { get; set; }
        public int duration { get; set; }
        public string customerName { get; set; }
        public string customerPhone { get; set; }
        

    }
}
