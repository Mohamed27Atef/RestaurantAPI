using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.RecipeImageRespository
{
    public class RecipeImageRespository:IRecipeImageRespository
    {
        private readonly RestaurantContext Context;
        public RecipeImageRespository(RestaurantContext context)
        {
            Context = context;
        }

        public void Add(RecipeImage entity)
        {
            Context.RecipeImages.Add(entity);
        }

        public void Delete(int id)
        {
            var recipeImg = GetById(id);
            Context.RecipeImages.Remove(recipeImg);
        }

        public List<RecipeImage> GetAll(string include = "")
        {
            var query = Context.RecipeImages.AsQueryable();
            if (!String.IsNullOrEmpty(include))
            {
                var includes = include.Split(",");
                foreach (var inc in includes)
                {
                    query = query.Include(inc.Trim());
                }
            }
            return query.ToList();
        }

        public RecipeImage GetById(int id)
        {
            return Context.RecipeImages.Find(id);
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void Update(RecipeImage entity)
        {
            Context.RecipeImages.Update(entity); ;
        }
    }
}
