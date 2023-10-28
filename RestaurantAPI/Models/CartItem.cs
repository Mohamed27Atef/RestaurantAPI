using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

      
        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal TotalPrice { get; set; }



        [Required]
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public virtual Cart? Cart { get; set; }

        [Required]
        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }
        public virtual Recipe? Recipe { get; set; }

        [Required]
        [ForeignKey("Resturant")]
        public int ResturantId { get; set; }
        public virtual Resturant? Resturant { get; set; }
    }
}
