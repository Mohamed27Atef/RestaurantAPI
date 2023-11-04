using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;

namespace RestaurantAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RestaurantContext context;

        public CategoryRepository(RestaurantContext context)
        {
            this.context = context;
        }

        public void Add(Cateigory entity)
        {
            context.Cateigorys.Add(entity);
        }

        public void Delete(int id)
        {
            var category = GetById(id);
            context.Cateigorys.Remove(category);
        }

        public List<Cateigory> GetAll(string include = "")
        {
            var query = context.Cateigorys.AsQueryable();
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

        public Cateigory GetById(int id)
        {
            return context.Cateigorys.Find(id);
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void Update(Cateigory entity)
        {
            context.Cateigorys.Update(entity);
        }
    }
}
