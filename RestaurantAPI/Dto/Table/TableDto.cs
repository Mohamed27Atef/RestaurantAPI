using RestaurantAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Dto
{
    public class TableDto
    {
        public DateTime dateTime { get; set; } = DateTime.Now;

        public int RestaurantId { get; set; }

        public string TableType { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public int duration { get; set; }
    }
}
