using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.RestaurantCateigoryRespository
{
    public class RestaurantCateigoryRepository : IRestaurantCateigoryRepository
    {
        private readonly RestaurantContext Context;
        public RestaurantCateigoryRepository(RestaurantContext context)
        {
            Context = context;
        }
        public void Add(RestaurantCateigory entity)
        {
            Context.RestaurantCateigories.Add(entity);
        }

        public void Delete(int id)
        {
            var cat = GetById(id);
            Context.RestaurantCateigories.Remove(cat);
        }

        public List<RestaurantCateigory> GetAll(string include = "")
        {
            var query = Context.RestaurantCateigories.AsQueryable();
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

        public RestaurantCateigory GetById(int id)
        {
            return Context.RestaurantCateigories.FirstOrDefault(c=> c.CategoryId == id);
        }
        public RestaurantCateigory GetByIdAndResutrantId(int categoryId,int resId)
        {
            return Context.RestaurantCateigories.FirstOrDefault(c => c.CategoryId == categoryId && c.RestaurantId == resId);
        }
        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void Update(RestaurantCateigory entity)
        {
            Context.RestaurantCateigories.Update(entity);
        }
    }
}
