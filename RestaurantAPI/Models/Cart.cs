using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public class Cart
    {
        public int id { get; set; }

      
        public virtual CartUser? CartUser { get; set; }

        public virtual Order? order { get; set; }
    }
}
