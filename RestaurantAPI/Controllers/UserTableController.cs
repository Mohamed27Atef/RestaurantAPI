using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Dto;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;

namespace RestaurantAPI.Controllers
{
    public class UserTableController : BaseApiClass
    {
        private readonly ITableUserRepository tableUserRepository;

        public UserTableController(ITableUserRepository tableUserRepository)
        {
            this.tableUserRepository = tableUserRepository;
        }

        [HttpGet]
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

    }
}
