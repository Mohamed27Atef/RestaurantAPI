using RestaurantAPI.Dto;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
using System.Data.Entity;

namespace RestaurantAPI.Repository.ResturantRepository
{
    public class ResturantRepo : IResturanrRepo
    {

        private readonly RestaurantContext Context;
        public ResturantRepo(RestaurantContext context)
        {
            Context = context;
        }

        public void add(Resturant entity)
        {
            Context.Resturants.Add(entity);
        }

        public void delete(int id)
        {
            Resturant res = Context.Resturants.FirstOrDefault(r => r.id == id);
            Context.Resturants.Remove(res);
        }

        public List<Resturant> getAll(string include = "")
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

        public Resturant getById(int id)
        {
            return Context.Resturants.FirstOrDefault(r => r.id == id);
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

        public void update(Resturant entity)
        {
            Context.Resturants.Update(entity);
        }

        public void UpdateIamge(string newUrl)
        {
            throw new NotImplementedException();
        }
    }
}
