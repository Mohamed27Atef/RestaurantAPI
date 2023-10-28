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
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Cateigory> GetAll(string include = "")
        {
            return context.Cateigorys.ToList();
        }

        public Cateigory GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(Cateigory entity)
        {
            throw new NotImplementedException();
        }
    }
}
