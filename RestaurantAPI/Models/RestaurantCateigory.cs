using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public class RestaurantCateigory
    {
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }

        //public Category Category { get; set; }
        //public Restaurant Restaurant { get; set; }
    }
}
