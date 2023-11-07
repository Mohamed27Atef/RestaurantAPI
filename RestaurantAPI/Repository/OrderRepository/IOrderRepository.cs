using E_Commerce.Repository;
using RestaurantAPI.Dto.Order;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.OrderRepository
{
    public interface IOrderRepository: IGenericRepository<Order>
    {
        
        public decimal GetOrderByIdTotalPrice(int id);
        public decimal GetAllOrderTotalPrice();
        public List<Order> getAllByUserId(int userId);
        IEnumerable<Order> getOrderByReataurantId(int reataurantId);
        int getStatusId(string status);
        List<UserOrderByRestaurantIdDto> getOrderOfUsresByRestaurant(int userId, ICartItemRepository cartItemRepository);
        Address getAddressByOrderId(int id);
        List<int> createRestaruantOrderStatus(int  cartId);
        int getCartIdByOrderId(int orderId);
    }
}
