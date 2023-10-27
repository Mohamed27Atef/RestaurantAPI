using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Dto;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;

namespace RestaurantAPI.Controllers
{
    public class TableController : BaseApiClass
    {
        private readonly ITableRepository tableRepository;
        private readonly ITableUserRepository tableUserRepository;

        public TableController(ITableRepository tableRepository, ITableUserRepository tableUserRepository)
        {
            this.tableRepository = tableRepository;
            this.tableUserRepository = tableUserRepository;
        }

        [HttpPost]
        public ActionResult createResrevatoinTable(TableDto table)
        {
            int table_id = tableRepository.isAvailable((TableType)table.TableType);
            
            if(table_id == -1)
                return BadRequest();

            // create table user
            UserTable userTable = new UserTable()
            {
                table_id = table_id,
                user_id = 1, // get the current user who logged 
            };

            tableRepository.createReservationTable(table_id);
            tableRepository.SaveChanges();
            tableUserRepository.add(userTable);
            tableUserRepository.SaveChanges();
            return NoContent();


        }
    }
}
