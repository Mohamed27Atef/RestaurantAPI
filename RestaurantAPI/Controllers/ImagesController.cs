using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{
    public class ImagesController : BaseApiClass
    {
        [HttpPut]
        public async Task<ActionResult> uploadrestaurantImage([FromForm] IFormFile image) {
            if (image != null && image.Length > 0)
            {

                var extention = Path.GetExtension(image.FileName);
                var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                var imageName = fileName + extention;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", imageName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
                return NoContent();
            }
            return BadRequest();
        }
    }
}
