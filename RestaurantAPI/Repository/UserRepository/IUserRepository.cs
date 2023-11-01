using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User getUserByApplicationUserId(string  applicationUserId);
        string getUserImage(int userId);
    }
}
