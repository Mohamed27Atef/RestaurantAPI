using RestaurantAPI.Models;

namespace RestaurantAPI.Dto.UserTable
{
    public class UserReservationDto
    {
        public int reservationNumber { get; set; }
        public DateTime dateTime { get; set; } = DateTime.Now;
        public int tableNumber { get; set; }
        public string restaurantName { get; set; }
        public TableType tableType { get; set; }

    }
}
