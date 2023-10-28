using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.OrderRepository
{
    public interface IOrderRepository: IGenericRepository<Order>
    {
        public List<Order> GetByLocation(string Location);
        public decimal GetOrderByIdTotalPrice(int id);
        public decimal GetAllOrderTotalPrice();
    }
}
