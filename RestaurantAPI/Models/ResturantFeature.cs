using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class ResturantFeature
    {
        public int FeatureId { get; set; }
        public Feature Feature { get; set; }

        public int ResturantId { get; set; }
        public Resturant Resturant { get; set; }
    }
}
