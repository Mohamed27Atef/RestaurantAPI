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
            return Context.RecipeImages.FirstOrDefault(r=>r.RecipeId == id);
        }
        public RecipeImage GetByIdAndImgReceipe(int id,string img)
        {
            return Context.RecipeImages.FirstOrDefault(r=> r.RecipeId == id && r.Image == img);
        }
        public List<RecipeImage> GetAllByIdReceipe(int id)
        {
            var recipe = Context.RecipeImages.Where(r => r.RecipeId == id).ToList();
            if (recipe != null)
                return recipe;
            return null;
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
