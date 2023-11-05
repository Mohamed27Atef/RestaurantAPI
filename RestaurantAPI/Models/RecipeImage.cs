using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RestaurantAPI.Models
{
    public class RecipeImage
    {
        public string Image { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        //[JsonIgnore]
        public virtual Recipe Recipe { get; set; }
    }
}
