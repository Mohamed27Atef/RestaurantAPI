using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Dto;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Repository.ResturantRepository
{
    public class ResturantRepo : IResturanrRepo
    {

        private readonly RestaurantContext Context;
        public ResturantRepo(RestaurantContext context)
        {
            Context = context;
        }

        public void Add(Resturant entity)
        {
            Context.Resturants.Add(entity);
        }

        public void Delete(int id)
        {
            Resturant res = Context.Resturants.FirstOrDefault(r => r.id == id);
            Context.Resturants.Remove(res);
        }

        public List<Resturant> GetAll(string include = "")
        {
            var query = Context.Resturants.AsQueryable();
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

        public Resturant getByAddress(string address)
        {
            return Context.Resturants.FirstOrDefault(r => r.Address == address);

        }

        public IEnumerable<ResturantDto> getByCategoryId(int category_id)
        {
            return Context.RestaurantCateigories.Where(ca => ca.CategoryId == category_id).Include(c => c.Resturant).Select(t => MapRestaurantToDtoService.mapResToDto(t.Resturant)).ToList();
            
        }

        public Resturant GetById(int id)
        {
            return Context.Resturants.Where(r => r.id == id)
                //.Include(r => r.Recipes)
                //.Include(r => r.resturantFeedbacks)
                .Include(r => r.ClosingDays)
                //.Include(r => r.RestaurantImages)
                .FirstOrDefault();
        }

        public List<string> getResaurantIamges(int restaruantId)
        {
            return Context.RestaurantImages.Where(r => r.restaurantId == restaruantId).Select(r => r.imageUrl).ToList();
        }



        public List<ResturantDto> getByName(string name)
        {
            return Context.Resturants.Where(res => res.Name.Contains(name)).Select(t => MapRestaurantToDtoService.mapResToDto(t)).ToList();
        }

        public List<ResturantDto> getByNameAndCategoryId(string name, int categoryId)
        {
          if (categoryId == 0)
                return Context.Resturants.Where(res => res.Name.Contains(name)).Select(t => MapRestaurantToDtoService.mapResToDto(t)).ToList();
            return getByCategoryId(categoryId).Where(res => res.Name.Contains(name)).ToList();

        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void Update(Resturant entity)
        {
            Context.Resturants.Update(entity);
        }

        public void UpdateIamge(string newUrl)
        {
            throw new NotImplementedException();
        }
    }
}
