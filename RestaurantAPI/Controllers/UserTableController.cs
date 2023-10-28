using Microsoft.AspNetCore.Mvc;
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
            
            return Ok(restaurantTalbleUser);
        }

    }
}
