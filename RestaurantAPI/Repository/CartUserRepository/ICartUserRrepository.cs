using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository
{
    public interface ICartUserRrepository : IGenericRepository<CartUser>
    {
        Cart getCartByUserId(int userId);
    }
}
