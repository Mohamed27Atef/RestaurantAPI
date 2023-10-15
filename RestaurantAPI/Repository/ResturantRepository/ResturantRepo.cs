using RestaurantAPI.Models;
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

        public Resturant getById(int id)
        {
            return Context.Resturants.FirstOrDefault(r => r.id == id);
        }

        public Resturant getByName(string name)
        {
            return Context.Resturants.FirstOrDefault(r => r.Name == name);
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
