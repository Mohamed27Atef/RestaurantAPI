using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RecipeId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal TotalPrice { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public int CartId { get; set; }

        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }

        //[ForeignKey("RestaurantId")]
        //public Restaurant Restaurant { get; set; }

        //[ForeignKey("CartId")]
        //public Cart Cart { get; set; }
    }
}
