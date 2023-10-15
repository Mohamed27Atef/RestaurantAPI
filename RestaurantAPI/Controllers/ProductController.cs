using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Repository.ProductRepository;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
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
