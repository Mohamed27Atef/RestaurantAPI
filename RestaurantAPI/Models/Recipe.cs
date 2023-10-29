using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public class Recipe
    {

        public int id { get; set; }

        [Required]
        [MaxLength(255)]
        public string name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        //[DataType(DataType.Date)]
        //public DateTime TimeToGet { get; set; }

        [ForeignKey("restaurant")]
        public int restaurantId { get; set; }

        public virtual Resturant? restaurant { get; set; }

        [ForeignKey("RecipeCategory")]
        public int recipeCategoryId { get; set; }

        public virtual RecipeCategory? RecipeCategory { get; set; }

        public virtual List<RecipeFeedback>? RecipeFeedbacks { get; set; }

        public virtual List<RecipeImage>? recipteImages{ get; set; } = new List<RecipeImage>();



    }
}
