namespace RestaurantAPI.Models
{
    public class Cateigory
    {
        public int Id { get; set; } 
        public string title { get; set; }

        public virtual List<Recipe>? Recipes { get; set; } = new List<Recipe>();
    }
}
