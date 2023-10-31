using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository
{
    public class TableRepository : ITableRepository
    {
        private readonly RestaurantContext context;

        public TableRepository(RestaurantContext context)
        {
            this.context = context;
        }

        public void Add(Table entity)
        {
            throw new NotImplementedException();
        }

        public int isAvailable(TableType tableType)
        {
            Table table = context.Tables.Where(t => t.TableType == tableType && t.AvailableState == AvailableState.Available).FirstOrDefault();
            if (table == null)
                return -1;
            return table.Id;
        }

        public void createReservationTable(int table_id)
        {
            Table table = context.Tables.Find(table_id);
            table.AvailableState = AvailableState.Reserved;
            context.Tables.Update(table);
            SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Table> GetAll(string include = "")
        {
            throw new NotImplementedException();
        }

        public Table GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public bool tableIsAvailable(int table_id)
        {
            Table table = context.Tables.Find(table_id);
            if (table.AvailableState == AvailableState.Available)
                return true;
            return false;
        }

        public void Update(Table entity)
        {
            throw new NotImplementedException();
        }

        public List<Table> getAvailableTaleInThisTime(DateTime time, int restaurantId)
        {

              return  context.Tables.Include(r => r.UserTable).Where(t => t.ResturantId == restaurantId && (t.UserTable.dateTime.Date != time.Date  || (t.UserTable.dateTime.Date == time.Date && t.UserTable.dateTime.Hour > time.Hour || t.UserTable.dateTime.Hour + t.UserTable.duration < time.Hour))).ToList();
        }
    }
}
