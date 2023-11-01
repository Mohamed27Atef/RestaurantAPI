using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.CuponRepository
{
    public class CuponRepository : ICuponRepository
    {
        private readonly RestaurantContext context;

        public CuponRepository(RestaurantContext context)
        {
            this.context = context;
        }
        public void Add(Copon entity)
        {
            context.Copons.Add(entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Copon> GetAll(string include = "")
        {
            throw new NotImplementedException();
        }

        public Copon GetById(int id)
        {
            return context.Copons.FirstOrDefault(c=> c.id == id);
        }
        public Copon GetByName(string name)
        {
            return context.Copons.FirstOrDefault(c => c.Text == name);
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void Update(Copon entity)
        {
            context.Copons.Update(entity);
        }
    }
}
