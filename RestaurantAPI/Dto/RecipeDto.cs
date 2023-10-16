using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RestaurantAPI.Models;
namespace RestaurantAPI.Dto
{
    public class RecipeDto
    {
            public int Id { get; set; }

            [Required]
            [MaxLength(255)]
            public string Name { get; set; }

            [MaxLength(1000)]
            public string Description { get; set; }

            [Required]
            [Column(TypeName = "money")]
            public decimal Price { get; set; }

            [DataType(DataType.Date)]
            public DateTime TimeToGet { get; set; }

            [ForeignKey("Restaurant")]
            public int RestaurantId { get; set; }

            public virtual Resturant? Restaurant { get; set; }

            [ForeignKey("Category")]
            public int CategoryId { get; set; }

            public virtual Cateigory? Category { get; set; }

            public virtual List<RecipeImage>? RecipeImages { get; set; } = new List<RecipeImage>();
        }
}
