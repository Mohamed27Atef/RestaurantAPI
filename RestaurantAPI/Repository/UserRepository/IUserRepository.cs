using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User getUserByApplicationUserId(string  applicationUserId);
        string getUserImage(int userId);
        Task UpdateProfileAsync(string userName, string userId, string firstName, string lastName, string email, string Location, string phoneNumber);

    }
}
