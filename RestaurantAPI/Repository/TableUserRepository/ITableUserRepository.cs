using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository
{
    public interface ITableUserRepository : IGenericRepository<UserTable>
    {
        List<UserTable> GetAllByRestaurantId(int restaurantId);
    }
}
