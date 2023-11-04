using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{
    public class ImagesController : BaseApiClass
    {
        [HttpPut]
        public void uploadrestaurantImage([FromBody] IFormFile theFile) {
            var file = Request.Form.Files[0];
        }
    }
}
