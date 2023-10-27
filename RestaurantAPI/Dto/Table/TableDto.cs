using RestaurantAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Dto
{
    public class TableDto
    {
        public int Id { get; set; }

        public int NumberOfPeople { get; set; }

        public int RestaurantId { get; set; }
        public int user_id { get; set; }

        public TableType TableType { get; set; }
    }
}
