using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public class UserTable
    {
        public int id { get; set; }
        [ForeignKey("user")]
        public int user_id { get; set; }
        [ForeignKey("Table")]
        public int table_id { get; set; }

        [ForeignKey("resturant")]
        public int restaurnatId { get; set; } = 1;
        public virtual Resturant? resturant { get; set; }

        public int duration { get; set; }

        public DateTime dateTime { get; set; } = DateTime.Now;
        public string name { get; set; }
        public string  phone { get; set; }


        public virtual User? user { get; set; }
        public virtual Table? Table { get; set; }
    }
}
