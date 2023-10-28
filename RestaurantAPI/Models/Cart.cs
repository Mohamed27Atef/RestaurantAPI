using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public class Cart
    {
        public int id { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal totalPrice { get; set; } = 0m;
        
        public virtual CartUser? CartUser { get; set; }

        public virtual Order? order { get; set; }
    }
}
