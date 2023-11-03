using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using RestaurantAPI.Dto;
using RestaurantAPI.Dto.Table;
using RestaurantAPI.Dto.UserTable;
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

            int table_id = tableRepository.getIdByTableType((TableType)tableRepository.getIntValueOfTableType(table.TableType), table.RestaurantId);
            

            // create table user
            UserTable userTable = new UserTable()
            {
                user_id = userRepository.getUserByApplicationUserId(GetUserIdFromClaims()).id, // get the current user who logged
                dateTime = table.dateTime,
                name = table.name,
                phone = table.phone,
                duration = table.duration,
                restaurnatId = table.RestaurantId,
                table_id = table_id,
                
            };
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

        [HttpGet("getReservationByrestaurantId/{restaurantId}")]
        public ActionResult getReservationByrestaurantId(int restaurantId)
        {
            List<UserTable> userTables = tableUserRepository.GetAllByRestaurantId(restaurantId);
            List<RestauarantAdminReservationDto> restauarantAdminReservationDtos = new();
            foreach (var item in userTables)
            {
                restauarantAdminReservationDtos.Add(new RestauarantAdminReservationDto()
                {
                    customerName = item.name,
                    customerPhone = item.phone,
                    dateTime = item.dateTime,
                    duration = item.duration,
                    reservationNumber = item.id,
                    tableNumber = item.table_id,
                    tableType = item.Table.TableType.ToString()
                });
            }

            return Ok(restauarantAdminReservationDtos);
        }


    }
}