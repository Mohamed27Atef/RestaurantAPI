using RestaurantAPI.Models;
using RestaurantAPI.Repository;

namespace RestaurantAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly RestaurantContext context;

        public UserRepository(RestaurantContext context)
        {
            this.context = context;
        }

        public void add(User entity)
        {
            context.Users.Add(entity);
        }

        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> getAll(string include = "")
        {
            throw new NotImplementedException();
        }

        public User getById(int id)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
