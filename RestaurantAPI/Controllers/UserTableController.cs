using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Dto;
using RestaurantAPI.Dto.UserTable;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;

namespace RestaurantAPI.Controllers
{
    public class UserTableController : BaseApiClass
    {
        private readonly ITableUserRepository tableUserRepository;
        private readonly IUserRepository userRepository;

        public UserTableController(ITableUserRepository tableUserRepository,
            IUserRepository userRepository)
        {
            this.tableUserRepository = tableUserRepository;
            this.userRepository = userRepository;
        }

        [HttpGet("searchByrestaurant/{restaurantId}")]
        public ActionResult searchByRestauarntId(int restaurantId)
        {
            List<UserTable> restaurantTalbleUser = tableUserRepository.GetAllByRestaurantId(restaurantId);

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
                }) ; 
            }

            return Ok(userTableDto);
        }

        [HttpGet("searchByUserId")]
        [Authorize]
        public ActionResult searchByUserId()
        {
            List<UserTable> userReservation = tableUserRepository.GetAllByUserId(userRepository.getUserByApplicationUserId(GetUserIdFromClaims()).id);

            // map list of usertable to usertabledto
            List<UserReservationDto> userTableDto = new();
            foreach (var item in userReservation)
            {
                userTableDto.Add(new UserReservationDto()
                {
                    dateTime = item.dateTime,
                    restaurantName = item.resturant.Name,
                    tableType = item.Table.TableType,
                    reservationNumber = item.id,
                    tableNumber = item.table_id,
                });
            }

            return Ok(userTableDto);
        }

    }
}
