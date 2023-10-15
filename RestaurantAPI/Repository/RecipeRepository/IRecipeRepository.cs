using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository
{
    public interface IRecipeRepository : IGenericRepository <Recipe>
    {
        Recipe getByRestaurantId(int restaurantId);
        Recipe getByCategoryId(int categoryId);
    }
}
