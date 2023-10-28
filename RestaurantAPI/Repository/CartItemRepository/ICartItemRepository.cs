using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        List<CartItem> GetAllByCartId(int cartId);
    }
}
