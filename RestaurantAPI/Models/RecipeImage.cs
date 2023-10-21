using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public class RecipeImage
    {
        public string Image { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
