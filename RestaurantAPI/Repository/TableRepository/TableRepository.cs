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

        public void add(Table entity)
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

        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Table> getAll(string include = "")
        {
            throw new NotImplementedException();
        }

        public Table getById(int id)
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

        public void update(Table entity)
        {
            throw new NotImplementedException();
        }
    }
}
