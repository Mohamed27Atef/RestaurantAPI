
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Dto;
using RestaurantAPI.Dto.results;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;
using RestaurantAPI.Repository.CartRepository;
using RestaurantAPI.Repository.RecipeImageRespository;
using RestaurantAPI.Repository.ResturantRepository;

namespace RestaurantAPI.Controllers
{

    public class RecipeController : BaseApiClass
    {
        private readonly IUserRepository userRepository;
        private readonly ICartItemRepository cartItemRepository;
        private readonly ICartRepository cartRepository;
        private readonly IResturanrRepo resturanrRepo;
        private readonly IMenuRepository menuRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRecipeImageRespository iRecipeImageRespository;
        private readonly IUserRepository IUserRepository;


        public RecipeController(IRecipeRepository recipeRepository,
            IRecipeImageRespository iRecipeImageRespository,
            IUserRepository userRepository,
            ICartItemRepository cartItemRepository,
            ICartRepository cartRepository,
            IResturanrRepo resturanrRepo,
            IMenuRepository menuRepository,
              IUserRepository _IUserRepository)
        {
            this._recipeRepository = recipeRepository;
            this.iRecipeImageRespository = iRecipeImageRespository;
            this.userRepository = userRepository;
            this.cartItemRepository = cartItemRepository;
            this.cartRepository = cartRepository;
            this.resturanrRepo = resturanrRepo;
            this.menuRepository = menuRepository;
            this.IUserRepository = _IUserRepository;

        }

        [HttpGet("search/{name}")]
        public IActionResult SearchRecipesByName(string name, [FromQuery] int p = 1)
        {
            const int pageSize = 10;
            int skip = (p - 1) * pageSize;
            var recipes = _recipeRepository.GetByName(name)
                .Skip(skip)
                .Take(pageSize)
                
        .Select(recipe => new RecipeDto
        {   Id=recipe.id,
            Name = recipe.name,
            Description = recipe.Description,
            Price = recipe.Price,
            imageUrl = recipe.imageUrl,
            rate = recipe.rate,
            restaurantId = recipe.Menu.restaurantId,
            restaurantName = recipe.Menu.restaurant.Name,
            menuName = recipe.Menu.title 
        })

        .Take(pageSize).ToList();

            if (recipes.Count == 0)
            {
                return NotFound();
            }

            return Ok(recipes);
        }

        [HttpGet("getMostRated")]
        public ActionResult getMostRated() {
            List<MostRated> mostrated = new List<MostRated>();
            foreach (var item in _recipeRepository.getMostRated())
            {
                mostrated.Add(new MostRated()
                {
                    Description = item.Description,
                    Price = item.Price,
                    id = item.id,
                    imageUrl = item.imageUrl,
                    Name = item.name,
                    rate=item.rate
                });
            }
            return Ok(mostrated);
        }


        [HttpGet("searchReceipeInResturant/{name}")]
        [Authorize]
        public IActionResult searchReceipeInResturant(string name, [FromQuery] int p = 1)
        {
            string userId = IUserRepository.getUserByApplicationUserId(GetUserIdFromClaims()).application_user_id;
            const int pageSize = 10;
            int skip = (p - 1) * pageSize;
            var recipes = _recipeRepository.GetByName(name)
                    .Where(r => r.Menu.restaurant.ApplicationIdentityUserID == userId)
            .Select(recipe => new RecipeDto
            {
                Id = recipe.id,
                Name = recipe.name,
                Description = recipe.Description,
                Price = recipe.Price,
                imageUrl = recipe.imageUrl,
                rate = recipe.rate,
                restaurantId = recipe.Menu.restaurantId,
                restaurantName = recipe.Menu.restaurant.Name,
                menuName = recipe.Menu.title
            }).
            ToList().Skip(skip).Take(pageSize);

            if (recipes.Count() == 0)
            {
                return NotFound();
            }

            return Ok(recipes);
        }



        [HttpGet("getRecipeByMenuId/{menuId}")]
        public IActionResult GetRecipesByMenuId(int menuId, [FromQuery] int p = 1)
        {
            const int pageSize = 10;
            int skip = (p - 1) * pageSize;
            var recipes = _recipeRepository.getByMenuId(menuId)
                .Skip(skip)
                .Take(pageSize).ToList();
            if (recipes == null)
                return NotFound();

            List<RecipeDto> recipeDtos = new List<RecipeDto>();
            foreach (var item in recipes)
                recipeDtos.Add(new RecipeDto()
                {
                    Description = item.Description,
                    Name = item.name,
                    Price = item.Price,
                    imageUrl = item.imageUrl,
                    Id= item.id,
                    
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
                Id = id,
                Description = recipe.Description,
                imageUrl = recipe.imageUrl,
                Name = recipe.name,
                Price = recipe.Price,
                menuName = recipe.Menu.title,
                restaurantId = recipe.Menu.restaurantId,
                restaurantName = recipe.Menu.restaurant.Name,
                images = _recipeRepository.getRecipeImages(id),
                rate = recipe.rate,
                menuId = recipe.menuId
            };


            foreach (var item in recipe.recipteImages)
                recipeDto.images.Add(item.Image);


            return Ok(recipeDto);

        }


        [HttpPost()]
        [Authorize()]
        public ActionResult CreateRecipe([FromBody] RecipeDto recipeDto)
        {
            if (recipeDto == null)
                return BadRequest("Invalid data");

            Recipe resc = _recipeRepository.GetById(recipeDto.Id);

            if (resc != null)
            {
                ActionResult res1 = UpdateRecipe(recipeDto);
                return res1;
            }

            //Menu menu = new Menu()
            //{
            //    restaurantId= ResutantId,
            //    title = menuRepository.GetById(recipeDto.menuId).title
            //};
            //menuRepository.Add(menu);

            Recipe recipe = new Recipe()
            {
                //recipteImages = recipeDto.RecipeImages,
                Description = recipeDto.Description,
                name = recipeDto.Name,
                Price = recipeDto.Price,
                imageUrl = recipeDto.imageUrl,
                menuId = recipeDto.menuId,
            };

            _recipeRepository.Add(recipe);
           int Raws= _recipeRepository.SaveChanges();

            if (Raws > 0)
            {
                
                    RecipeImage resMainImg = new RecipeImage { RecipeId = recipe.id, Image = recipeDto.imageUrl };
                    this.iRecipeImageRespository.Add(resMainImg);
                    //this.iRecipeImageRespository.SaveChanges();
                

                foreach (var imagUrl in recipeDto.images)
                {
                    var imgReceipeFound = this.iRecipeImageRespository.GetByIdAndImgReceipe(recipe.id, imagUrl);
                    if (imgReceipeFound is null)
                    {
                        RecipeImage resImg = new RecipeImage { RecipeId = recipe.id, Image = imagUrl };
                        this.iRecipeImageRespository.Add(resImg);
                        this.iRecipeImageRespository.SaveChanges();
                    }
                }

                return NoContent();// get the url.....
            }
            return NotFound("Recipe creation failed.");

        }

        [Authorize]
        [HttpGet("IsRecipeAddedToCart/{recipeId}")]
        public ActionResult<bool> IsRecipeAddedToCart(int recipeId)
        {
            int userId = userRepository.getUserByApplicationUserId(GetUserIdFromClaims()).id;
            Cart cart = cartRepository.GetNonOrderedCartByUserId(userId);
            bool isAddedToCart;
            if (cart == null)
            {
                isAddedToCart = false;
                return isAddedToCart;
            }
            else
            {
                CartItem cartItem = cartItemRepository.GetByCartIdAndRecipeId(cart.id, recipeId);
                if(cartItem==null)
                {
                    isAddedToCart = false;
                    return isAddedToCart;
                }
                else
                {
                    isAddedToCart = true;
                    return isAddedToCart;
                }
            }
        }

        [HttpPut()]
        public ActionResult UpdateRecipe( [FromBody] RecipeDto recipeDto)
        {
            if (recipeDto == null)
                return BadRequest("Invalid data");



            var existingRecipe = _recipeRepository.GetById(recipeDto.Id);
            if (existingRecipe == null)
                return NotFound();

            //existingRecipe.recipteImages = recipeDto.RecipeImages;
            existingRecipe.menuId = recipeDto.menuId;
            existingRecipe.Description = recipeDto.Description;
            existingRecipe.name = recipeDto.Name;
            existingRecipe.Price = recipeDto.Price;

            _recipeRepository.Update(existingRecipe);
            int Raws = _recipeRepository.SaveChanges();

            if(Raws > 0)
            {
               
                var AllImage = iRecipeImageRespository.GetAllByIdReceipe(existingRecipe.id);
                
                    

                RecipeImage resMainImg = new RecipeImage { RecipeId = existingRecipe.id, Image = recipeDto.imageUrl };
                this.iRecipeImageRespository.Add(resMainImg);

                return Ok("ok");
            }

            return NotFound("Receipe Not Found!");
        }


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
