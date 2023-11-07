using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public class Resturant
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Restaurant name is required.")]
        public string Name { get; set; }

        public string? Address { get; set; }

        [Required(ErrorMessage = "Cuisine type is required.")]
        public string? Cusinetype { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180.")]
        public decimal? Longitude { get; set; }

        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90.")]
        public decimal? Latitude { get; set; }

        [Range(1, 5, ErrorMessage = "Rate must be between 1 and 5.")]
        public decimal? Rate { get; set; }

        [Range(0, 24, ErrorMessage = "Open hours must be between 0 and 24.")]
        public decimal OpenHours { get; set; }

        [Range(0, 24, ErrorMessage = "Closing hours must be between 0 and 24.")]
        public decimal ClosingHours { get; set; }


        [Required]
        public string email { get; set; } // check if it is a valid email
        public string  Password { get; set; }

        public string Image { get; set; }
        public string phone { get; set; }

        [ForeignKey("ApplicationIdentityUser")]
        public string? ApplicationIdentityUserID { get; set; }
        public ApplicationIdentityUser? ApplicationIdentityUser { get; set; }

        public virtual List<RestaurantCateigory>? Cateigories { get; set; } = new List<RestaurantCateigory>();

        public virtual List<Menu>? Menus { get; set;} = new List<Menu>();

        public virtual List<ResturantFeedback>? resturantFeedbacks { get; set; } = new List<ResturantFeedback>();

        public virtual List<Table>? Tables { get; set; } = new List<Table>();
        
        
        public virtual List<DeliveryMan>? DeliveryMen { get; set; } = new List<DeliveryMan>();

        public virtual List<ResturantFeature>? ResturantFeatures { get; set; } = new List<ResturantFeature>();
        public virtual List<UserTable>? UserTables { get; set; } = new List<UserTable>();
        public virtual List<ClosingDay>? ClosingDays { get; set; } = new List<ClosingDay>();
        public virtual List<RestaurantImage>? RestaurantImages { get; set; } = new List<RestaurantImage>();

        public virtual List<RestaruantOrdersStatus>? RestaruantOrdersStatus { get; set; } = new List<RestaruantOrdersStatus>();



    }
}
