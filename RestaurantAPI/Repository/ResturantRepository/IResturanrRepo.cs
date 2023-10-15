using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.ResturantRepository
{
    public interface IResturanrRepo:IGenericRepository<Resturant>
    {
        public Resturant getByAddress(string address);
        public Resturant getByName(string name);
        public void UpdateIamge(string newUrl);
    }
}
