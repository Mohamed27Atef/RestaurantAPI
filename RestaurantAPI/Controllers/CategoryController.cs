using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Repository;

namespace RestaurantAPI.Controllers
{
    public class CategoryController : BaseApiClass
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public ActionResult categories()
        {
            return Ok(categoryRepository.GetAll());
        }
    }
}
