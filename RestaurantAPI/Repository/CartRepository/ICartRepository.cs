using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.CartRepository
{
    public interface ICartRepository:IGenericRepository<Cart>
    {
        Cart getCatByOrderId(int orderId);
        Cart getCartByUserId(int userId);
        Cart GetNonOrderedCartByUserId(int userId);
    }
}
