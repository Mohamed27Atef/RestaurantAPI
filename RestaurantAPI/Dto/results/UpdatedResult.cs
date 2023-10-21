using RestaurantAPI.Models;

namespace RestaurantAPI.Dto.results
{
    public class UpdatedRecipeResult
    {
        public Recipe reicpe { get; set; }
        public string? msg { get; set; }
    }
}
