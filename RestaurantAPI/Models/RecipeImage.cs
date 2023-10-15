namespace RestaurantAPI.Models
{
    public class RecipeImage
    {
        public int RecipeId { get; set; }
        public string Image { get; set; }

        public Recipe Recipe { get; set; }
    }
}
