using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository
{
    public class TableUserRepository : ITableUserRepository
    {
        private readonly RestaurantContext context;

        public TableUserRepository(RestaurantContext context)
        {
            this.context = context;
        }
        public void Add(UserTable entity)
        {
            context.UserTables.Add(entity);
        }

        public void Delete(int id)
        {
            UserTable userTable = GetById(id);
            context.UserTables.Remove(userTable);
        }

        public List<UserTable> GetAll(string include = "")
        {
            throw new NotImplementedException();
        }

        public List<UserTable> GetAllBy(int restaurantId)
        {
            return context.UserTables.Where(r => r.restaurnatId == restaurantId).ToList();
        }

        public List<UserTable> GetAllByRestaurantId(int restaurantId)
        {
            return context.UserTables.Where(r => r.restaurnatId == restaurantId).Include(r => r.resturant).Include(r => r.Table).ToList();
        }

        public List<UserTable> GetAllByUserId(int userId)
        {
            return context.UserTables.Where(r => r.user_id == userId).Include(r => r.resturant).Include(r => r.Table).ToList();
        }

        public UserTable GetById(int id)
        {
            return context.UserTables.Find(id); 
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void Update(UserTable entity)
        {
            throw new NotImplementedException();
        }
    }
}
