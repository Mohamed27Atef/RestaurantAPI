using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using RestaurantAPI.Dto;
using RestaurantAPI.Dto.UserTable;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;

namespace RestaurantAPI.Controllers
{
    public class UserTableController : BaseApiClass
    {
        private readonly ITableUserRepository tableUserRepository;
        private readonly ITableRepository tableRepository;
        private readonly IUserRepository userRepository;

        public UserTableController(ITableUserRepository tableUserRepository,
            ITableRepository tableRepository,
            IUserRepository userRepository)
        {
            this.tableUserRepository = tableUserRepository;
            this.tableRepository = tableRepository;
            this.userRepository = userRepository;
        }

        [HttpGet("searchByrestaurant/{restaurantId}")]
        public ActionResult searchByRestauarntId(int restaurantId, [FromQuery] int p = 1)
        {
            const int pageSize = 10;
            int skip = (p - 1) * pageSize;
            List<UserTable> restaurantTalbleUser = tableUserRepository
                .GetAllByRestaurantId(restaurantId)
                .Skip(skip)
                .Take(pageSize).ToList();

            // map list of usertable to usertabledto
            List<UserTableDto> userTableDto = new(); 
            foreach (var item in restaurantTalbleUser)
            {
                userTableDto.Add(new UserTableDto()
                {
                    dateTime = item.dateTime,
                    name = item.name,
                    phone = item.phone,
                    reservationNumber = item.id,
                    tableNumber = item.table_id,
                    duration = item.duration,
                }) ; 
            }

            return Ok(userTableDto);
        }

        [HttpGet("searchByUserId")]
        //[Authorize]
        public ActionResult searchByUserId([FromQuery]int p = 1 )
        {

            const int pageSize = 10;
            int skip = (p - 1) * pageSize;

            List<UserTable> userReservation = tableUserRepository.GetAllByUserId(userRepository
                .getUserByApplicationUserId(GetUserIdFromClaims()).id)
                .Skip(skip)
                .Take(pageSize).ToList();

            // map list of usertable to usertabledto
            List<UserReservationDto> userTableDto = new();
            foreach (var item in userReservation)
            {
                userTableDto.Add(new UserReservationDto()
                {
                    
                    reservationNumber = item.id,
                    tableNumber = item.table_id,
                    dateTime = item.dateTime,
                    tableType = tableRepository.GetById(item.table_id).TableType.ToString(),
                    restaurantName = item.resturant.Name,
                    duration = item.duration
                });
            }


            return Ok(userTableDto);
        }


        [HttpDelete]
        public ActionResult deleteUserTable(int id)
        {
            if (id < 0)
            {
                return BadRequest("Invalid UserTable id.");
            }

            var res = tableUserRepository.GetById(id);
            if (res is  null)
                return NotFound("UserTable Not Found!");

            tableUserRepository.Delete(id);
            int Raws = tableUserRepository.SaveChanges();
            if (Raws > 0)
            {
                return NoContent();
            }

            return NotFound("UserTable updated failed.");
        }



    }
}
