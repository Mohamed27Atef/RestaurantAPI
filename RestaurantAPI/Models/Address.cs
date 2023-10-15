using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Street { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; }

        public virtual Order Orders { get; set; }
    }
}
