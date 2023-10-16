
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;
namespace RestaurantAPI.Controllers
{

    public class RecipeController : BaseApiClass
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeController(IRecipeRepository recipeRepository)
        {
            this._recipeRepository = recipeRepository;
        }
      
        [HttpGet("api/recipe/restaurant/{restaurantId}")]
        public IActionResult GetRecipesByRestaurant(int restaurantId)
        {
            var recipes = _recipeRepository.getByRestaurantId(restaurantId);

            if (recipes == null)
            {
                return NotFound();
            }

            return Ok(recipes);
        }

        
        [HttpGet("api/recipe/category/{categoryId}")]
        public IActionResult GetRecipesByCategory(int categoryId)
        {
            var recipes = _recipeRepository.getByCategoryId(categoryId);

            if (recipes == null)
            {
                return NotFound();
            }

            return Ok(recipes);
        }

        [HttpGet("api/recipe/{id}")]
        public IActionResult GetRecipe(int id)
        {
            var recipe = _recipeRepository.getById(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
           
        }

       
        [HttpPost("api/recipe")]
        public IActionResult CreateRecipe([FromBody] Recipe recipe)
        {
            if (recipe == null)
            {
                return BadRequest("Invalid data");
            }

            
            recipe.TimeToGet = DateTime.Now;

            _recipeRepository.add(recipe);

            return CreatedAtAction("GetRecipe", new { id = recipe.id }, recipe);
        }

        
        [HttpPut("api/recipe/{id}")]
        public IActionResult UpdateRecipe(int id, [FromBody] Recipe recipe)
        {
            if (recipe == null || recipe.id != id)
            {
                return BadRequest("Invalid data");
            }

            var existingRecipe = _recipeRepository.getById(id);
            if (existingRecipe == null)
            {
                return NotFound();
            }

            _recipeRepository.update(recipe);

            return NoContent();
        }

        
        [HttpDelete("api/recipe/{id}")]
        public IActionResult DeleteRecipe(int id)
        {
            var recipe = _recipeRepository.getById(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _recipeRepository.delete(id);

            return NoContent();
        }
    }
}
