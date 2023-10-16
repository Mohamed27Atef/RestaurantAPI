using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class Feature
    {
        public Feature() 
        { 
            ResturantFeatures = new List<ResturantFeature>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(255)] 
        public string Name { get; set; }

        [StringLength(1000)] 
        public string Description { get; set; }
        public virtual List<ResturantFeature>? ResturantFeatures { get; set; }

    }
}
