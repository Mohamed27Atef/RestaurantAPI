using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository.CuponRepository
{
    public interface ICuponRepository:IGenericRepository<Copon>
    {
        public Copon GetByName(string name);
    }
}
