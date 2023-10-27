using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository
{
    public interface ITableRepository : IGenericRepository<Table>
    {

        bool createReservationTable(int table_id);
        bool tableIsAvailable(int table_id);

    }
}
