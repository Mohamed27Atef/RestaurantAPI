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
        Family,
        Solo,
    }
    public class Table
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int NumberOfPeople { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public AvailableState AvailableState { get; set; }

        [Required]
        public TableType TableType { get; set; }

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }


    }
    }

