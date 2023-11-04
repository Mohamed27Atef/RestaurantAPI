using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public enum OrderStatus
    {
        processed = 0,
        shipped = 1,
        enRoute = 2,
        arrived = 3,
        Canceled = 4
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

  
        [Required]
        [Column(TypeName = "money")]
        public decimal TotalPrice { get; set; }


        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }


        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public virtual Address? Address { get; set; }

        //public virtual Copon? copon { get; set; }


    }
}
