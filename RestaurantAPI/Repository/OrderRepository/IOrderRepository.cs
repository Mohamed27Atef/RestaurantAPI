using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.OrderRepository
{
    public interface IOrderRepository: IGenericRepository<Order>
    {
        
        public decimal GetOrderByIdTotalPrice(int id);
        public decimal GetAllOrderTotalPrice();
        public List<Order> getAllByUserId(int userId);
        IEnumerable<Order> getOrderByReataurantId(int reataurantId);
    }
}
