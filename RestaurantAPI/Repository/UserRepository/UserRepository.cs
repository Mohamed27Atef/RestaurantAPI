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

        public void Add(User entity)
        {
            context.Users.Add(entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll(string include = "")
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User getUserByApplicationUserId(string applicationUserId)
        {
            return context.Users.Where(u => u.application_user_id == applicationUserId).FirstOrDefault();
        }

        public string getUserImage(int userId)
        {
            return context.Users.Where(r => r.id == userId).Select(u => u.Image).FirstOrDefault();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
