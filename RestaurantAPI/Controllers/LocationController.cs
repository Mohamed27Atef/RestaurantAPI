using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Repository;

namespace RestaurantAPI.Controllers
{
    public class LocationController :BaseApiClass
    {
        private readonly ILocationRepository locationRepository;

        public LocationController(ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        [HttpGet]
        public ActionResult getAllLocation()
        {
            return Ok(locationRepository.getAllLocatoin());
        }
    }
}
