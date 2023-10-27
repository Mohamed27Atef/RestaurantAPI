using RestaurantAPI.Models;

namespace RestaurantAPI.Dto.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public decimal TotalPrice { get; set; }
       
        public string Location { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public int UserId { get; set; }
        public int? DeliveryId { get; set; }
        public int CartId { get; set; }
        public int? AddressId { get; set; }
    }

}
