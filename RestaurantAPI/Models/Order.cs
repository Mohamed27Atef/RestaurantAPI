using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Shipped,
        Delivered,
        Canceled
    }

    public enum DeliveryMethod
    {
        Standard,
        Express,
        PickUp
    }

    public enum PaymentMethod
    {
        CreditCard,
        VodafoneCash,
        CashOnDelivery
    }

    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal TotalPrice { get; set; }

        public int? DeliveryId { get; set; }

        [Required]
        public DeliveryMethod DeliveryMethod { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [MaxLength(255)]
        public string Location { get; set; }

        public DateTime? DeliveryTime { get; set; }

        [Required]
        public int CartId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("DeliveryId")]
        public DeliveryMan Deliveryman { get; set; }

        [ForeignKey("CartId")]
        public CartItem CartItem { get; set; }

    }
}
