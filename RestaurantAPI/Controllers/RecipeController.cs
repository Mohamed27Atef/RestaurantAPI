using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Repository;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository productRepository;

        public RecipeController(IRecipeRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        [HttpGet]
        public ActionResult Index()
        {
            return Ok("test");
        }
        // get by restaurantId


        // get by id


        // get CateogryId


        // create Recipe


        // edite recipe


        // delete recipe
    }
}
