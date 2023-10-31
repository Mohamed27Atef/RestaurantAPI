using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository
{
    public interface ITableRepository : IGenericRepository<Table>
    {

        void createReservationTable(int table_id);
        int isAvailable(TableType tableType);
        int getIdByTableType(TableType tableType);
        IEnumerable<Table> getAvailableTaleInThisTime(DateTime time, int restaurantId);
    }
}
