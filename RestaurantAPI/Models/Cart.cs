using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public class Cart
    {
        public int id { get; set; }

        [ForeignKey("user")]
        public string userId { get; set; }

        public virtual User? user { get; set; }

        public virtual Order? order { get; set; }
    }
}
