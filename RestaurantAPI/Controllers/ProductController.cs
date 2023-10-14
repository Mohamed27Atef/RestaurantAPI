using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Repository.ProductRepository;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
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
