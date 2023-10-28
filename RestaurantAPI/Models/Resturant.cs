using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class Resturant
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Restaurant name is required.")]
        public string Name { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "Cuisine type is required.")]
        public string? Cusinetype { get; set; }

        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180.")]
        public decimal? Longitude { get; set; }

        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90.")]
        public decimal? Latitude { get; set; }

        [Range(1, 5, ErrorMessage = "Rate must be between 1 and 5.")]
        public decimal? Rate { get; set; }

        [Range(0, 24, ErrorMessage = "Open hours must be between 0 and 24.")]
        public decimal OpenHours { get; set; }

        public string Image { get; set; }

        public virtual List<RestaurantCateigory>? Cateigories { get; set; } = new List<RestaurantCateigory>();

        public virtual List<Recipe>? Recipes { get; set;} = new List<Recipe>();

        public virtual List<ResturantFeedback>? resturantFeedbacks { get; set; } = new List<ResturantFeedback>();

        public virtual List<Table>? Tables { get; set; } = new List<Table>();
        
        
        public virtual List<DeliveryMan>? DeliveryMen { get; set; } = new List<DeliveryMan>();

        public virtual List<ResturantFeature>? ResturantFeatures { get; set; } = new List<ResturantFeature>();
        public virtual List<UserTable>? UserTables { get; set; } = new List<UserTable>();




    }
}
