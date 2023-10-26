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

        public void add(Cateigory entity)
        {
            throw new NotImplementedException();
        }

        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Cateigory> getAll(string include = "")
        {
            return context.Cateigorys.ToList();
        }

        public Cateigory getById(int id)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void update(Cateigory entity)
        {
            throw new NotImplementedException();
        }
    }
}
