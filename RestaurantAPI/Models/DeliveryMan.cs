using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class DeliveryMan
    {
        [Key]
        public int Id { get; set; }

        //[Required]
        //[MaxLength(255)]
        //public string Name { get; set; }

        //[Required]
        //[Phone]
        //public string Phone { get; set; }

        //[Required]
        //[MaxLength(255)]
        //public string Location { get; set; }

        //[Required]
        //[ForeignKey("Resturant")]
        //public int ResturantId { get; set; }
        //public virtual Resturant? Resturant { get; set; }

    }
}
