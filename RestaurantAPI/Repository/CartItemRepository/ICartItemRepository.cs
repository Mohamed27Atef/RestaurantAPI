using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        List<CartItem> GetAllByCartId(int cartId);
        List<CartItem> GetByCartIdAndRestaurantId(int cartId, int restaurantId);
        List<CartItem> GetAllByCartIdAndRestaurantId(int cartId, int restaurantId);
        decimal getTotalPriceOrderByRestaurantIdAndOrderId(int cartId, int restaurantId);
        public CartItem GetByCartIdAndRecipeId(int cartId, int recipeId);
    }
}
