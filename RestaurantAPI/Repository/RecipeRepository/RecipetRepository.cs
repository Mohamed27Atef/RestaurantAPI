using RestaurantAPI.Models;
using System.Data.Entity;

namespace RestaurantAPI.Repository.ProductRepository
{
    public class RecipetRepository : IRecipeRepository
    {
        private readonly RestaurantContext Context;
        public RecipetRepository(RestaurantContext context)
        {
            Context = context;
        }


        public void add(Recipe entity)
        {
            throw new NotImplementedException();
        }

        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Recipe> getAll(string include = "")
        {
            var recipes = Context.Recipes.Include(include).ToList();
            if (recipes != null)
                return recipes;
            return null;
        }

        public Recipe getByCategoryId(int categoryId)
        {
            var recipe = Context.Recipes.FirstOrDefault(r => r.categoryId == categoryId);
            if (recipe != null)
                return recipe;
            return null;
        }

        public Recipe getById(int id)
        {
            var recipe = Context.Recipes.FirstOrDefault(r=>r.id == id);
            if (recipe != null)
                return recipe;
            return null;
        }

        public Recipe getByRestaurantId(int restaurantId)
        {
            var recipe = Context.Recipes.FirstOrDefault(r => r.restaurantId == restaurantId);
            if (recipe != null)
                return recipe;
            return null;
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void update(Recipe entity)
        {
            throw new NotImplementedException();
        }
    }
}
