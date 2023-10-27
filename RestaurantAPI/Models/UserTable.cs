using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public class UserTable
    {
        [ForeignKey("user")]
        public int user_id { get; set; }
        [ForeignKey("Table")]
        public int table_id { get; set; }

        public virtual User? user { get; set; }
        public virtual Table? Table { get; set; }
    }
}
