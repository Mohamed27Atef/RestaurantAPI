using RestaurantAPI.Models;

namespace RestaurantAPI.Dto.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
       
        public DateTime? DeliveryTime { get; set; }
        public int UserId { get; set; }
        public int? AddressId { get; set; }
    }

}
