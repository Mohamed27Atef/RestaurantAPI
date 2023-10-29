namespace RestaurantAPI.Models
{
    public class RecipeCategory
    {
        public int id { get; set; }
        public string title { get; set; }
        public virtual List<Recipe>? Recipes { get; set; } = new List<Recipe>();
    }
}
