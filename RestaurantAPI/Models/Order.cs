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
        public OrderStatus Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal TotalPrice { get; set; }

        [Required]
        public DeliveryMethod DeliveryMethod { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [MaxLength(255)]
        public string Location { get; set; }
      

        public DateTime? DeliveryTime { get; set; }

        


        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }

        [Required]
        [ForeignKey("Deliveryman")]
        public int? DeliveryId { get; set; }
        public virtual DeliveryMan? Deliveryman { get; set; }

        [Required]
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public virtual Cart? Cart { get; set; }


        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public virtual Address? Address { get; set; }


    }
}
