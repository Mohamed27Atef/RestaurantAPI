using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.RestaurantOrderStatus
{
    public interface IRestaurantOrderStatus : IGenericRepository<RestaruantOrdersStatus>
    {
        OrderStatus getStatusByRestaurntIdCartId(int restaurntId, int CartId);
        void updateStatus(int restaurntId, int CartId, OrderStatus status);
    }
}
