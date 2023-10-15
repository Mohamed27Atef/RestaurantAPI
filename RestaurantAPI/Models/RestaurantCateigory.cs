using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public class RestaurantCateigory
    {
        [ForeignKey("Cateigory")]
        public int CategoryId { get; set; }

        public virtual Cateigory? Cateigory { get; set; }


        [ForeignKey("Resturant")]
        public int RestaurantId { get; set; }

        public virtual Resturant? Resturant { get; set; }


    }
}
