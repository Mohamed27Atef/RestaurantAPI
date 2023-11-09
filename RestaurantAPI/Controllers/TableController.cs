using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using RestaurantAPI.Dto;
using RestaurantAPI.Dto.Table;
using RestaurantAPI.Dto.UserTable;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;
using RestaurantAPI.Repository.ResturantRepository;
using System.Drawing.Printing;

namespace RestaurantAPI.Controllers
{
    public class TableController : BaseApiClass
    {
        private readonly ITableRepository tableRepository;
        private readonly ITableUserRepository tableUserRepository;
        private readonly IUserRepository userRepository;
        private readonly IResturanrRepo iResturanrRepo;
        public TableController(
            ITableRepository tableRepository,
            ITableUserRepository tableUserRepository,
            IUserRepository userRepository,
            IResturanrRepo iResturanrRepo)
        {
            this.tableRepository = tableRepository;
            this.tableUserRepository = tableUserRepository;
            this.userRepository = userRepository;
            this.iResturanrRepo = iResturanrRepo;
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
            var table = tableRepository.getAvailableTaleInThisTime(time, restaurantId)
                .DistinctBy(r => r.TableType.ToString()).ToList(); 

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

        [HttpGet("getReservationByrestaurantId")]
        [Authorize]
        public ActionResult getReservationByrestaurantId([FromQuery] int p = 1)
        {
            string AppId = GetUserIdFromClaims();
            Resturant resturant = iResturanrRepo.getByAppId(AppId);
            const int pageSize = 10;
            int skip = (p - 1) * pageSize;
            List<UserTable> userTables = tableUserRepository
                .GetAllByRestaurantId(resturant.id)
                  .Skip(skip)
                .Take(pageSize)
                .ToList();

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
                    tableType = tableRepository.GetById(item.table_id).TableType.ToString(),
                });
            }

            return Ok(restauarantAdminReservationDtos);
        }

        [HttpPost("createTable")]
        [Authorize]
        public ActionResult CreateTable(TablerestaurantDto tableDto)
        {
            string userId = GetUserIdFromClaims();
            int ResutantId = iResturanrRepo.getByUserId(userId).id;
            Table newTable = new Table
            {
                TableType = (TableType)Enum.Parse(typeof(TableType), tableDto.tableType),
                ResturantId = ResutantId,
            };

            tableRepository.Add(newTable);
            tableRepository.SaveChanges();
                
            return Ok(tableDto);
        }


    }
}