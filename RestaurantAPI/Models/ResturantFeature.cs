using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public class ResturantFeature
    {
        [ForeignKey("Feature")]
        [Required]
        public int FeatureId { get; set; }
        public virtual Feature? Feature { get; set; }

        [ForeignKey("Resturant")]
        [Required]
        public int ResturantId { get; set; }
        public virtual Resturant? Resturant { get; set; }
    }
}
