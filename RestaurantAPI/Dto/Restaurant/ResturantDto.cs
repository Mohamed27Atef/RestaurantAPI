using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Dto
{
    public class ResturantDto
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Restaurant name is required.")]
        public string Name { get; set; }

        public string email { get; set; }
        public string Password { get; set; }

        public string? Description { get; set; }
        public string? Address { get; set; }

        [Required(ErrorMessage = "Cuisine type is required.")]
        public string Cusinetype { get; set; }

        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180.")]
        public decimal? Longitude { get; set; }

        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90.")]
        public decimal? Latitude { get; set; }

        [Range(1, 5, ErrorMessage = "Rate must be between 1 and 5.")]
        public decimal? Rate { get; set; }

        [Range(0, 24, ErrorMessage = "Open hours must be between 0 and 24.")]
        public decimal OpenHours { get; set; }


        public decimal ClosingHours { get; set; }
        public string phone { get; set; }

        public string? Image { get; set; }

        public List<string>? images { get; set; } = new List<string>();
        public List<int>? RestaurantCategories { get; set; } = new List<int>();

    }
}
