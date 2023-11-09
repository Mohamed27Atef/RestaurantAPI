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
            context.Tables.Add(entity);
        }

        public int isAvailable(TableType tableType)
        {
            Table table = context.Tables.Where(t => t.TableType == tableType).FirstOrDefault();
            return table.Id;
        }

        public void createReservationTable(int table_id)
        {
            Table table = context.Tables.Find(table_id);
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
            return context.Tables.Where(t => t.Id == id).FirstOrDefault();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void Update(Table entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Table> getAvailableTaleInThisTime(DateTime time, int restaurantId)
        {

            var r = context.Tables.Include(r => r.UserTable).Where(t => t.ResturantId == restaurantId && (t.UserTable.dateTime.Date != time.Date 
            || (t.UserTable.dateTime.Date == time.Date && 
            !(t.UserTable.dateTime.Hour == time.Hour 
            && t.UserTable.dateTime.Hour + t.UserTable.duration > time.Hour)))).ToList();
            return r;

        }

        public int getIdByTableType(TableType tableType, int restaurantId)
        {
            return context.Tables.Where(t => t.ResturantId == restaurantId && t.TableType == tableType).Select(r => r.Id).FirstOrDefault();
        }

        public int getIntValueOfTableType(string type)
        {


            switch (type) {
                case "Family": return 0;
                case "Solo": return 1;
                case "Mini": return 2;
                case "Medium": return 3;
                default : return -1;

            }
        }
    }
}
