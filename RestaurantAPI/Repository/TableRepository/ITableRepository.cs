using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository
{
    public interface ITableRepository : IGenericRepository<Table>
    {

        void createReservationTable(int table_id);
        bool tableIsAvailable(int table_id);
        int isAvailable(TableType tableType);

    }
}
