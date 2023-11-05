using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.RestaurantImageRepository
{
    public interface IRestaurantImageRepository:IGenericRepository<RestaurantImage>
    {
        public List<RestaurantImage> GetAllById(int id);
        public RestaurantImage GetByIdAndImg(int id, string img);
    }
}
