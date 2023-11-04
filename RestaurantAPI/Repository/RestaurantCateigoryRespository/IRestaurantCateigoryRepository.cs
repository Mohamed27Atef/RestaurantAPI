using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.RestaurantCateigoryRespository
{
    public interface IRestaurantCateigoryRepository:IGenericRepository<RestaurantCateigory>
    {
        public RestaurantCateigory GetByIdAndResutrantId(int categoryId, int resId);
    }
}
