using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class ResturantFeature
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Feature name is required.")]
        public string Name { get; set; }
    }
}
