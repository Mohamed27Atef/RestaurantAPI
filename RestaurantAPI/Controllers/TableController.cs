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
        private readonly IUserRepository userRepository;

        public TableController(
            ITableRepository tableRepository,
            ITableUserRepository tableUserRepository,
            IUserRepository userRepository)
        {
            this.tableRepository = tableRepository;
            this.tableUserRepository = tableUserRepository;
            this.userRepository = userRepository;
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
                user_id = userRepository.getUserByApplicationUserId(GetUserIdFromClaims()).id, // get the current user who logged
                dateTime = table.dateTime,
                name = table.name,
                phone = table.phone

            };

            tableRepository.createReservationTable(table_id);
            tableRepository.SaveChanges();
            tableUserRepository.Add(userTable);
            tableUserRepository.SaveChanges();
            return NoContent();


        }
    }
}
