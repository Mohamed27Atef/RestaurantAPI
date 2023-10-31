using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public enum AvailableState
    {
        Available,
        Occupied,
        Reserved
    }

    public enum TableType
    {
        Family = 0, // for family
        Solo = 1, // 1
        Mini = 2, // 2-3
        Medium = 3, // 4 - 5
    }
    public class Table
    {

        [Key]
        public int Id { get; set; }

        //[Required]
        //public int table_number { get; set; }


        //[Required]
        //public AvailableState AvailableState { get; set; } = AvailableState.Available;

        [Required]
        public TableType TableType { get; set; }

        public virtual UserTable? UserTable { get; set; }

        [Required]
        [ForeignKey("Resturant")]
        public int ResturantId { get; set; }
        public virtual Resturant? Resturant { get; set; }

    }
}

