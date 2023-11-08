using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository
{
    public interface IRecipeRepository : IGenericRepository <Recipe>
    {
        //Recipe getByRestaurantId(int restaurantId);
        List<Recipe> getByMenuId(int menuId);
        List<string> getRecipeImages(int id);
        List<Recipe> GetByName(string name);
        List<Recipe> getMostRated();


    }
}
