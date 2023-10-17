
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Dto;
using RestaurantAPI.Dto.results;
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
      
        [HttpGet("{restaurantId}")]
        public IActionResult GetRecipesByRestaurant(int restaurantId)
        {
            var recipes = _recipeRepository.getByRestaurantId(restaurantId);

            if (recipes == null)
            {
                return NotFound();
            }

            return Ok(recipes);
        }

        
        [HttpGet("getByCateigory/{categoryId}")]
        public IActionResult GetRecipesByCategory(int categoryId)
        {
            var recipes = _recipeRepository.getByCategoryId(categoryId);

            if (recipes == null)
            {
                return NotFound();
            }

            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public IActionResult GetRecipe(int id)
        {
            var recipe = _recipeRepository.getById(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
           
        }

       
        [HttpPost()]
        public ActionResult CreateRecipe([FromBody] RecipeDto recipeDto)
        {
            if (recipeDto == null)
                return BadRequest("Invalid data");

            Recipe recipe = new Recipe()
            {
                //recipteImages = recipeDto.RecipeImages,
                categoryId = recipeDto.CategoryId,
                restaurantId = recipeDto.RestaurantId,
                Description = recipeDto.Description,
                name = recipeDto.Name,
                Price = recipeDto.Price,
            };

            _recipeRepository.add(recipe);
            _recipeRepository.SaveChanges();

            return Created("",recipe); // get the url.....
        }


        [HttpPut("{id}")]
        public ActionResult<ResultsDto> UpdateRecipe(int id, [FromBody] RecipeDto recipeDto)
        {
            if (recipeDto == null)
                return BadRequest("Invalid data");



            var existingRecipe = _recipeRepository.getById(id);
            if (existingRecipe == null)
                return NotFound();

            //existingRecipe.recipteImages = recipeDto.RecipeImages;
            existingRecipe.categoryId = recipeDto.CategoryId;
            existingRecipe.restaurantId = recipeDto.RestaurantId;
            existingRecipe.Description = recipeDto.Description;
            existingRecipe.name = recipeDto.Name;
            existingRecipe.Price = recipeDto.Price;


            _recipeRepository.update(existingRecipe);
            _recipeRepository.SaveChanges();
            return new ResultsDto()
            {
                statusCode = 200,
                msg = "updated success",
                data = new UpdatedResult()
                {
                    reicpe = existingRecipe
                }
            };
        }

        
        [HttpDelete("{id}")]
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
