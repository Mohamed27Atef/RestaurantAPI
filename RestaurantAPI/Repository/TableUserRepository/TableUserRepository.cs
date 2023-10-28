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
        public void add(UserTable entity)
        {
            context.UserTables.Add(entity);
        }

        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserTable> getAll(string include = "")
        {
            throw new NotImplementedException();
        }

        public List<UserTable> GetAllByRestaurantId(int restaurantId)
        {
            return context.UserTables.Where(r => r.restaurnatId == restaurantId).ToList();
        }

        public UserTable getById(int id)
        {
            return context.UserTables.Find(id);
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void update(UserTable entity)
        {
            throw new NotImplementedException();
        }
    }
}
