using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.OrderRepository
{
    public interface IOrderRepository: IGenericRepository<Order>
    {
        
        public decimal GetOrderByIdTotalPrice(int id);
        public decimal GetAllOrderTotalPrice();
    }
}
