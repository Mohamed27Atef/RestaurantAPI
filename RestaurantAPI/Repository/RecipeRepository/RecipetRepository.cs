using RestaurantAPI.Models;

namespace RestaurantAPI.Repository
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
            throw new NotImplementedException();
        }

        public Recipe getByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Recipe getById(int id)
        {
            throw new NotImplementedException();
        }

        public Recipe getByRestaurantId(int restaurantId)
        {
            throw new NotImplementedException();
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
