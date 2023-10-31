using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using RestaurantAPI.Dto;
using RestaurantAPI.Dto.Table;
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
        [Authorize]
        public ActionResult createResrevatoinTable(TableDto table)
        {
            int table_id = tableRepository.getIdByTableType((TableType)table.TableType);
            

            // create table user
            UserTable userTable = new UserTable()
            {
                table_id = table_id,
                user_id = userRepository.getUserByApplicationUserId(GetUserIdFromClaims()).id, // get the current user who logged
                dateTime = table.dateTime,
                name = table.name,
                phone = table.phone,
                duration = table.duration,
            };

            //tableRepository.createReservationTable(table_id);
            //tableRepository.SaveChanges();
            tableUserRepository.Add(userTable);
            tableUserRepository.SaveChanges();
            return NoContent();


        }

        [HttpGet("getAvailableTalbe")]
        public ActionResult getAvailableTaleInThisTime(DateTime time, int restaurantId)
        {
            var table = tableRepository.getAvailableTaleInThisTime(time, restaurantId).DistinctBy(r => r.TableType.ToString());

            List<TablerestaurantDto> tableDtos = new List<TablerestaurantDto>();

            foreach (var item in table)
            {
                tableDtos.Add(new TablerestaurantDto()
                {
                    id = item.Id,
                    tableType = item.TableType.ToString()
                }) ;
            }

            return Ok(tableDtos);
        }


    }
}
