using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Dto
{
    public class GetOneRestaurantDto
    {
        public int id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Cusinetype { get; set; }

        public decimal? Longitude { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Rate { get; set; }

        public decimal OpenHours { get; set; }
        public decimal closingHours { get; set; }

        public string Image { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public List<string> images { get; set; } = new List<string>();
        public List<string> clossingDays { get; set; } = new List<string>();
    }
}
