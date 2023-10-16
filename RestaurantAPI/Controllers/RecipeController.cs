
using RestaurantAPI.Repository;
namespace RestaurantAPI.Controllers
{

    public class RecipeController : BaseApiClass
    {
        private readonly IRecipeRepository productRepository;

        public RecipeController(IRecipeRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        // get by restaurantId


        // get by id


        // get CateogryId


        // create Recipe


        // edite recipe


        // delete recipe
    }
}
