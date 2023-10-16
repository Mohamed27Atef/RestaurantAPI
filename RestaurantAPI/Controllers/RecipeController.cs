
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;
namespace RestaurantAPI.Controllers
{

    public class RecipeController : BaseApiClass
    {
        private readonly IRecipeRepository recipeRepository;

        public RecipeController(IRecipeRepository recipeRepository)
        {
            this.recipeRepository = recipeRepository;
        }

        [HttpGet]
        public ActionResult getAll()
        {
            var recipes = recipeRepository.getAll();
            if (recipes != null)
                return Ok(recipes);

            return NotFound();
        }

        [HttpGet("{id:int}")]
        public ActionResult getById(int id)
        {
            var recipe = recipeRepository.getById(id);
            if (recipe != null)
                return Ok(recipe);

            return NotFound();
        }

        [HttpGet("getByResturantId/{id:int}")]
        public ActionResult getByRestaurantId(int restaurantId)
        {
            var recipe = recipeRepository.getByRestaurantId(restaurantId);
            if (recipe != null) 
                return Ok(recipe);

            return NotFound();
        }

        
        [HttpGet("getByCatg/{id:int}")]
        public ActionResult getByCatagroyId(int catagroyId)
        {
            var recipe = recipeRepository.getByCategoryId(catagroyId);
            if (recipe != null)
                return Ok(recipe);

            return NotFound();
        }

        // create Recipe


        // edite recipe


        // delete recipe
    }
}
