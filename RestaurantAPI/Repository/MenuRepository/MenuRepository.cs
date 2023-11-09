using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly RestaurantContext context;

        public MenuRepository(RestaurantContext context)
        {
            this.context = context;
        }


        public void Add(Menu entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            context.Menus.Add(entity);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Menu> GetAll(string include = "")
        {
            var query = context.Menus.AsQueryable();
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

        public Menu GetById(int id)
        {
            return context.Menus.FirstOrDefault(r => r.id == id);
        }

        public List<Menu> GetByRestaurantId(int restuarantId)
        {
            return context.Menus.Where(t => t.restaurantId == restuarantId).Include(r => r.Recipes).ToList();
        }

        public Menu getByRestaurantIdTitle(int restartantId, string title)
        {
            return context.Menus.Where(r => r.restaurantId == restartantId && r.title == title).FirstOrDefault();
        }

        public List<Recipe> getMostRatedRecipe(int restaurantId)
        {
            var menus = context.Menus.Where(r => r.restaurantId == restaurantId).Include(r => r.Recipes);
            List<Recipe> mostRated = new List<Recipe>();
            foreach (var item in menus)
                mostRated.Add(item.Recipes.OrderByDescending(r => r.rate).FirstOrDefault());

            return mostRated;

        }

        public int SaveChanges()
        {
            return context.SaveChanges(); 
        }


        public void Update(Menu entity)
        {
            throw new NotImplementedException();
        }
    }
}
