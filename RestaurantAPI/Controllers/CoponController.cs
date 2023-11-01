using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Repository.CuponRepository;
using RestaurantAPI.Repository.ResturantRepository;

namespace RestaurantAPI.Controllers
{

    public class CoponController : BaseApiClass
    {
        private readonly ICuponRepository iCuponRepository;

        public CoponController(ICuponRepository iCuponRepository)
        {
            this.iCuponRepository = iCuponRepository;
        }

        [HttpGet]
        public IActionResult getByName(string name)
        {
            if(name is null)
            {
                return BadRequest("Faild to get Copon");
            }
            Copon copon = iCuponRepository.GetByName(name);

             if(copon is null)
            {
                return Ok(new Copon { DiscountPercentage = -1});
            }

            return Ok(copon);
        }

    }
}
