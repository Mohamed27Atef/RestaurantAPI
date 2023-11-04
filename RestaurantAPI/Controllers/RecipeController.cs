
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Dto;
using RestaurantAPI.Dto.results;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;
using RestaurantAPI.Repository.RecipeImageRespository;

namespace RestaurantAPI.Controllers
{

    public class RecipeController : BaseApiClass
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRecipeImageRespository iRecipeImageRespository;


        public RecipeController(IRecipeRepository recipeRepository, IRecipeImageRespository iRecipeImageRespository)
        {
            this._recipeRepository = recipeRepository;
            this.iRecipeImageRespository = iRecipeImageRespository;

        }


        [HttpGet("getRecipeByMenuId/{menuId}")]
        public IActionResult GetRecipesByMenuId(int menuId)
        {
            var recipes = _recipeRepository.getByMenuId(menuId);

            if (recipes == null)
                return NotFound();

            List<RecipeDto> recipeDtos = new List<RecipeDto>();
            foreach (var item in recipes)
                recipeDtos.Add(new RecipeDto()
                {
                    Description = item.Description,
                    Name = item.name,
                    Price = item.Price,
                    imageUrl = item.imageUrl
                });
            return Ok(recipeDtos);
        }

        //[HttpGet("{restaurantId}")]
        //public IActionResult GetAllRecipeByRestaurantId(int restaurantId)
        //{
        //    var recipes = _recipeRepository.getByMenuId(menuId);

        //    if (recipes == null)
        //        return NotFound();

        //    List<RecipeDto> recipeDtos = new List<RecipeDto>();
        //    foreach (var item in recipes)
        //        recipeDtos.Add(new RecipeDto()
        //        {
        //            Description = item.Description,
        //            Name = item.name,
        //            Price = item.Price,
        //            imageUrl = item.imageUrl
        //        });
        //    return Ok(recipeDtos);
        //}

        [HttpGet("{id}")]
        public IActionResult GetRecipe(int id)
        {
            var recipe = _recipeRepository.GetById(id);


            if (recipe == null)
                return NotFound();

            RecipeDto recipeDto = new RecipeDto()
            {
                Description = recipe.Description,
                imageUrl = recipe.imageUrl,
                Name = recipe.name,
                Price = recipe.Price,
                menuName = recipe.Menu.title
            };

            foreach (var item in recipe.recipteImages)
                recipeDto.images.Add(item.Image);


            return Ok(recipeDto);

        }


        [HttpPost()]
        public ActionResult CreateRecipe([FromBody] RecipeDto recipeDto)
        {
            if (recipeDto == null)
                return BadRequest("Invalid data");

            Recipe recipe = new Recipe()
            {
                //recipteImages = recipeDto.RecipeImages,
                Description = recipeDto.Description,
                name = recipeDto.Name,
                Price = recipeDto.Price,
                imageUrl = recipeDto.imageUrl,
                menuId = recipeDto.menuId
            };

            _recipeRepository.Add(recipe);
           int Raws= _recipeRepository.SaveChanges();

            if (Raws > 0)
            {

                foreach (var imagUrl in recipeDto.images)
                {
                    RecipeImage resImg = new RecipeImage { RecipeId = recipe.id, Image = imagUrl };
                    this.iRecipeImageRespository.Add(resImg);
                    this.iRecipeImageRespository.SaveChanges();
                }

                return Created("", recipe); // get the url.....
            }
            return NotFound("Recipe creation failed.");

        }


        //[HttpPut("{id}")]
        //public ActionResult<UpdatedRecipeResult> UpdateRecipe(int id, [FromBody] RecipeDto recipeDto)
        //{
        //    if (recipeDto == null)
        //        return BadRequest("Invalid data");



        //    var existingRecipe = _recipeRepository.GetById(id);
        //    if (existingRecipe == null)
        //        return NotFound();

        //    //existingRecipe.recipteImages = recipeDto.RecipeImages;
        //    existingRecipe.menuId = recipeDto.menuId;
        //    existingRecipe.Description = recipeDto.Description;
        //    existingRecipe.name = recipeDto.Name;
        //    existingRecipe.Price = recipeDto.Price;


        //    _recipeRepository.Update(existingRecipe);
        //    _recipeRepository.SaveChanges();
        //    return Ok(new UpdatedRecipeResult()
        //    {
        //        reicpe = existingRecipe
        //    });
        //}


        //[HttpDelete("{id}")]
        //public IActionResult DeleteRecipe(int id)
        //{
        //    var recipe = _recipeRepository.GetById(id);
        //    if (recipe == null)
        //    {
        //        return NotFound();
        //    }

        //    _recipeRepository.Delete(id);

        //    return NoContent();
        //}
    }
}
