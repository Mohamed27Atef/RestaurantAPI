using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.ProductRepository
{
    public interface IProductRepository : IGenericRepository <Product>
    {
        Product getByRestaurantId(int restaurantId);
        Product getByCategoryId(int categoryId);
    }
}
