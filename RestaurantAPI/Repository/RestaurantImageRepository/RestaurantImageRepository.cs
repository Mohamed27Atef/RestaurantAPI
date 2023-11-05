using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.RestaurantImageRepository
{
    public class RestaurantImageRepository: IRestaurantImageRepository
    {
        private readonly RestaurantContext Context;
        public RestaurantImageRepository(RestaurantContext context)
        {
            Context = context;
        }

        public void Add(RestaurantImage entity)
        {
            Context.RestaurantImages.Add(entity);
        }

        public void Delete(int id)
        {
            var resImg = GetById(id);
            Context.RestaurantImages.Remove(resImg);
        }

        public List<RestaurantImage> GetAll(string include = "")
        {
            var query = Context.RestaurantImages.AsQueryable();
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

        public RestaurantImage GetById(int id)
        {
            return Context.RestaurantImages.FirstOrDefault(r=> r.restaurantId == id);
        }
        public RestaurantImage GetByIdAndImg(int id,string img)
        {
            return Context.RestaurantImages.FirstOrDefault(r => r.restaurantId == id && r.imageUrl == img);
        }
        public List<RestaurantImage> GetAllById(int id)
        {
            return Context.RestaurantImages.Where(r => r.restaurantId == id).ToList();
        }
        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void Update(RestaurantImage entity)
        {
            Context.RestaurantImages.Update(entity);
        }
    }
}
